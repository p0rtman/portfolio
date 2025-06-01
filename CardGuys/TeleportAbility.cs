using UnityEngine;

public class TeleportAbility : MonoBehaviour, IAbility
{
    public float teleportDistance = 3f;
    public float cooldown = 10f;

    private float lastActivation;

    public bool CanActivate() =>
        Time.time >= lastActivation + cooldown;

    public void Activate(GameObject user, GameObject target)
    {
        if (!CanActivate()) return;
        lastActivation = Time.time;

        // Calculate random offset around the target
        Vector3 randomOffset = Random.onUnitSphere;
        randomOffset.y = 0;
        randomOffset = randomOffset.normalized * teleportDistance;

        Vector3 newPos = target.transform.position + randomOffset;
        // Optionally clamp to NavMesh if needed
        user.transform.position = newPos;

        Debug.Log($"{user.name} teleported near {target.name}");
    }
}
