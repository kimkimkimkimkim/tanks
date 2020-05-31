using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CpuTankMovementLandMark : MonoBehaviour {
    public float speed;
    [HideInInspector] public bool canMove;
    [HideInInspector] public GameObject targetObject;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    private void Update() {
        if(navMeshAgent.hasPath) navMeshAgent.isStopped = !canMove;
        if (navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid) {
            navMeshAgent.SetDestination(targetObject.transform.position);
        }
    }
}
