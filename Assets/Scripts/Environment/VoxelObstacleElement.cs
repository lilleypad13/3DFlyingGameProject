using System.Collections;
using UnityEngine;

public class VoxelObstacleElement : MonoBehaviour, IDestructible
{
    private Rigidbody rb;

    [SerializeField] private float repeatHitTimer = 2.0f;
    private bool canBeHit = true;
    private IEnumerator hitTimerCoroutine;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Damaged(Vector3 forceApplied)
    {
        if (canBeHit)
        {
            if(hitTimerCoroutine != null)
            {
                StopCoroutine(hitTimerCoroutine);
            }

            hitTimerCoroutine = HitTimer();
            rb.AddForce(forceApplied);
            StartCoroutine(hitTimerCoroutine);
        }
    }

    private IEnumerator HitTimer()
    {
        canBeHit = false;

        yield return new WaitForSeconds(repeatHitTimer);

        canBeHit = true;
    }

    public void EndLife()
    {
        //Destroy(this.gameObject);
        StopCoroutine(hitTimerCoroutine);
    }
}
