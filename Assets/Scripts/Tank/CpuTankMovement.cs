using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CpuTankMovement : TankMovement {
    [HideInInspector] public GameObject targetObject;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public bool enableControle = true;
    private float movingTime = 4;
    private float stoppingTime = 1;

    public override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(MovingLoop());
    }

    private void Update() {
        if (navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid) {
            navMeshAgent.SetDestination(targetObject.transform.position);
        }
    }

    private IEnumerator MovingLoop() {
        while (true) {
            yield return StartCoroutine(Move());
            yield return StartCoroutine(Stop());
        }
    }

    private IEnumerator Move() {
        navMeshAgent.isStopped = (enableControle) ? false : true;
        yield return new WaitForSeconds(movingTime);
    }

    private IEnumerator Stop() {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(stoppingTime);
    }
}
