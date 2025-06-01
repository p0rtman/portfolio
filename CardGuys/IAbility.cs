using UnityEngine;

public interface IAbility
{
    void Activate(GameObject user, GameObject target);
    bool CanActivate();
}
