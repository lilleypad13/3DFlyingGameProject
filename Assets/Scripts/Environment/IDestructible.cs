using UnityEngine;

public interface IDestructible
{
    void Damaged(Vector3 forceApplied);
    void EndLife();
}
