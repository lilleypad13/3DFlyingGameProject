using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        CollectibleRing.CollectedRing += CollectedObject;
    }

    private void OnDestroy()
    {
        CollectibleRing.CollectedRing -= CollectedObject;
    }

    private void CollectedObject(GameObject collectible)
    {
        Debug.Log("Collected:" + collectible.name);
        Destroy(collectible.gameObject);
    }
}
