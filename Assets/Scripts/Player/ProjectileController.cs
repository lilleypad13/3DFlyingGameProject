using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float maxProjectileSize = 1.0f;
    [SerializeField] private float projectileGrowthRate = 1.0f;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileSonicBoom projectileProperties = projectile.GetComponent<ProjectileSonicBoom>();

        if(projectileProperties != null)
        {
            projectileProperties.MaxSize = maxProjectileSize;
            projectileProperties.GrowthRate = projectileGrowthRate;
        }
        else
        {
            Debug.LogWarning("Check projectile controller for projectile spawning property references.");
        }
    }
}
