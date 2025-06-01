using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CardGuyAI : MonoBehaviour
{
    public Transform currentTarget;
    private NavMeshAgent agent;
    private CardGuyController controller;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CardGuyController>();
        agent.speed = controller.cardData.movementSpeed;
    }

    void Update()
    {
        if (currentTarget == null) return;

        agent.SetDestination(currentTarget.position);

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        if (distance <= agent.stoppingDistance + 0.1f)
            TryAttack();
    }

    void UpdateTarget()
    {
        CardGuyController[] allUnits = FindObjectsOfType<CardGuyController>();
        float closest = Mathf.Infinity;
        Transform best = null;

        foreach (var unit in allUnits)
        {
            if (unit == controller) continue;
            if (unit.gameObject.tag == gameObject.tag) continue;

            float dist = Vector3.Distance(transform.position, unit.transform.position);
            if (dist < closest)
            {
                closest = dist;
                best = unit.transform;
            }
        }

        if (best != currentTarget)
        {
            currentTarget = best;
            BattleLogger.Instance.Log($"{gameObject.name} ▶ New target: {currentTarget?.name}");
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime < controller.cardData.attackSpeed) return;
        lastAttackTime = Time.time;

        if (currentTarget == null) return;
        var enemy = currentTarget.GetComponent<CardGuyController>();
        if (enemy == null) return;

        int dmg = controller.cardData.damage;
        BattleLogger.Instance.Log($"{gameObject.name} attacks {enemy.name} for {dmg} damage");
        enemy.TakeDamage(dmg);

        // Activate attached abilities
        foreach (var ability in GetComponents<IAbility>())
            if (ability.CanActivate())
                ability.Activate(gameObject, enemy.gameObject);
    }

    // Resubscribe target updates each second
    void OnEnable() => InvokeRepeating(nameof(UpdateTarget), 0, 1f);
    void OnDisable() => CancelInvoke(nameof(UpdateTarget));
}
