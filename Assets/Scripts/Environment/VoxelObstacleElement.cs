using UnityEngine;

public class VoxelObstacleElement : MonoBehaviour, IDestructible
{
    public void Destroyed()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        //Destroy(this.gameObject);
    }
}
