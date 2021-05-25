using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRing : MonoBehaviour
{
    public static event Action<GameObject> CollectedRing;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something");

        if (other.gameObject.GetComponent<PlayerManager>().CurrentGoal == this.gameObject)
        {
            Debug.Log("Was proper target");
            CollectedRing(gameObject);
        }
    }

}
