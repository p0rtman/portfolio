using UnityEngine;
using System.Collections;

public class DoTAbility : MonoBehaviour, IAbility
{
    public GameObject vfxPrefab;
    public float duration = 5f;
    public float tickInterval = 1f;
    public int damagePerTick = 2;
    public float cooldown = 8f;
    private float lastActivation;

    public bool CanActivate() => Time.time >= lastActivation + cooldown;

    public void Activate(GameObject user, GameObject target)
    {
        if (!CanActivate()) return;
        lastActivation = Time.time;
        BattleLogger.Instance.Log($"{user.name} applies DoT to {target.name} ({duration}s, {damagePerTick}/tick)");
        StartCoroutine(ApplyDoT(target));
    }

    IEnumerator ApplyDoT(GameObject target)
    {
        if (vfxPrefab != null && target != null)
        {
            var fx = Instantiate(vfxPrefab, target.transform.position + Vector3.up, Quaternion.identity, target.transform);
            Destroy(fx, duration + 0.5f);
        }

        float endTime = Time.time + duration;
        var ctrl = target.GetComponent<CardGuyController>();

        while (Time.time < endTime && ctrl != null && ctrl.currentHealth > 0)
        {
            BattleLogger.Instance.Log($"→ DoT tick on {target.name}: {damagePerTick} damage");
            ctrl.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        BattleLogger.Instance.Log($"DoT ended on {target.name}");
    }
}
