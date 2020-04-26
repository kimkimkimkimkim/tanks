using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CpuTankMovement : TankMovement {
    [HideInInspector] public GameObject targetObject;

    private NavMeshAgent navMeshAgent;

    public override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid) {
            navMeshAgent.SetDestination(targetObject.transform.position);
        }
    }
}
