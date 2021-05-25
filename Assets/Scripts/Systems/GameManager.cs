using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static float GameTimer { get => gameTimer; }
    private static float gameTimer;

    private void Start()
    {
        gameTimer = 0.0f;
        CollectibleRing.CollectedRing += CollectedObject;
    }

    private void OnDestroy()
    {
        CollectibleRing.CollectedRing -= CollectedObject;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
    }

    private void CollectedObject(GameObject collectible)
    {
        Debug.Log("Collected:" + collectible.name);
        Destroy(collectible.gameObject);
    }
}
