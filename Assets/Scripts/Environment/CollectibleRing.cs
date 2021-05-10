using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRing : MonoBehaviour
{
    public static event Action<GameObject> CollectedRing;
    //public static event Action CollectedRing;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit something");

        if (other.gameObject.GetComponent<PlayerController_Flying>())
        {
            //CollectedRing();
            CollectedRing(gameObject);
        }
    }

    //private void Collected(GameObject ring)
    //{
    //    Debug.Log("Collected ring." + gameObject.name);
    //    Destroy(ring.gameObject);
    //}

    //private void Collected()
    //{
    //    Debug.Log("Collected ring." + gameObject.name);
    //    Destroy(gameObject);
    //}

}
