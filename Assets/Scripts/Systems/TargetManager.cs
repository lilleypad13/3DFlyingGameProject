using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static event Action MissionAccomplished;

    public GameObject FirstTarget
    {
        get => targetList[0];
    }
    [SerializeField] private List<GameObject> targetList;
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        CollectibleRing.CollectedRing += AssignNextTarget;
    }

    private void OnDestroy()
    {
        CollectibleRing.CollectedRing -= AssignNextTarget;
    }

    private void AssignNextTarget(GameObject currentTarget)
    {
        if(currentTarget != null)
        {
            targetList.Remove(currentTarget);
        }

        if (targetList.Count > 0)
        {
            playerManager.CurrentGoal = targetList[0];
        }
        else
        {
            // Player has completed target list
            Debug.Log("COMPLETED LIST OF GOALS");
            MissionAccomplished();
        }
    }
}
