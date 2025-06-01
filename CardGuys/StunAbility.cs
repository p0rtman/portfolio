using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class StunAbility : MonoBehaviour, IAbility
{
    public float stunDuration = 2f;
    public float cooldown = 5f;
    private float lastCast;

    public bool CanActivate() => Time.time >= lastCast + cooldown;

    public void Activate(GameObject user, GameObject target)
    {
        if (!CanActivate()) return;
        lastCast = Time.time;
        BattleLogger.Instance.Log($"{user.name} stuns {target.name} for {stunDuration}s");
        StartCoroutine(DoStun(target));
    }

    IEnumerator DoStun(GameObject target)
    {
        var agent = target.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(stunDuration);
            agent.isStopped = false;
        }
    }
}
