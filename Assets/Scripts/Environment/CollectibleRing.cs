using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRing : MonoBehaviour
{
    public static event Action<GameObject> CollectedRing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && 
            other.gameObject.GetComponent<PlayerManager>().CurrentGoal == this.gameObject)
        {
            CollectedRing(gameObject);
        }
    }

}
