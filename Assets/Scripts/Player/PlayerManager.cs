using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private TargetManager targetManager;

    public GameObject CurrentGoal { 
        get => currentGoal;
        set
        {
            currentGoal = value;
            currentGoal.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    [SerializeField] private GameObject currentGoal;

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void Start()
    {
        CurrentGoal = targetManager.FirstTarget;
    }
}
