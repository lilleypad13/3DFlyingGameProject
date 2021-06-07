using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSonicBoom : MonoBehaviour
{
    public float MaxSize
    {
        get => maxSize;
        set => maxSize = value;
    }
    private float maxSize = 1.0f;
    public float GrowthRate
    {
        get => growthRate;
        set => growthRate = value;
    }
    private float growthRate = 1.0f;
    public float ForceMultiplier
    {
        get => forceMultiplier;
        set => forceMultiplier = value;
    }
    private float forceMultiplier;

    private void Update()
    {
        float scaleValue = transform.localScale.x + growthRate * Time.deltaTime;
        if(scaleValue <= maxSize)
        {
            transform.localScale = Vector3.one * scaleValue;
        }
        else
        {
            EndProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDestructible destructibleCheck = other.GetComponent<IDestructible>();

        if (destructibleCheck != null)
        {
            destructibleCheck.Damaged(forceMultiplier * ImpactDirection(other.transform.position));
            //destructibleCheck.Destroyed();
        }
    }

    private Vector3 ImpactDirection(Vector3 targetPosition)
    {
        return Vector3.Normalize(targetPosition - transform.position);
    }

    private void EndProjectile()
    {
        Destroy(this.gameObject);
    }
}
