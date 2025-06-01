using UnityEngine;
using UnityEngine.AI;

public class CardGuyController : MonoBehaviour
{
    [HideInInspector] public CardData cardData;
    [HideInInspector] public GameObject healthBarPrefab;
    private HealthBarUI healthBarUI;
    private int maxHealth;
    public int currentHealth { get; private set; }

    /// <summary>Initialize stats and spawn the health bar.</summary>
    public void Initialize(CardData data)
    {
        cardData = data;
        maxHealth = data.health;
        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            var hb = Instantiate(healthBarPrefab, transform);
            hb.transform.localPosition = Vector3.up * 2f;
            healthBarUI = hb.GetComponent<HealthBarUI>();
            healthBarUI.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        BattleLogger.Instance.Log(
            $"{gameObject.name} took {amount} dmg → {currentHealth}/{maxHealth} HP");

        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        BattleLogger.Instance.Log($"{gameObject.name} died.");
        if (healthBarUI != null)
            Destroy(healthBarUI.gameObject);
        Destroy(gameObject);
    }

    /// <summary>Apply a single UpgradeData to this unit (old global upgrade system).</summary>
    public void ApplyUpgrade(UpgradeData upg)
    {
        switch (upg.type)
        {
            case UpgradeType.StatBoost:
                switch (upg.statToBoost)
                {
                    case CardStat.Health:
                        maxHealth += Mathf.CeilToInt(upg.boostAmount);
                        currentHealth = maxHealth;
                        if (healthBarUI != null)
                            healthBarUI.SetHealth(currentHealth, maxHealth);
                        break;
                    case CardStat.Damage:
                        cardData.damage += Mathf.CeilToInt(upg.boostAmount);
                        break;
                    case CardStat.Speed:
                        cardData.movementSpeed += upg.boostAmount;
                        var agent = GetComponent<NavMeshAgent>();
                        if (agent != null) agent.speed = cardData.movementSpeed;
                        break;
                    case CardStat.AttackSpeed:
                        cardData.attackSpeed = Mathf.Max(0.1f, cardData.attackSpeed - upg.boostAmount);
                        break;
                }
                break;

            case UpgradeType.NewAbility:
                if (upg.abilityToAdd != null)
                {
                    var list = new System.Collections.Generic.List<AbilityData>(cardData.abilities);
                    list.Add(upg.abilityToAdd);
                    cardData.abilities = list.ToArray();
                    // optionally AttachAbility here if desired
                }
                break;
        }

        BattleLogger.Instance.Log($"{gameObject.name} applied upgrade: {upg.upgradeName}");
    }
}
