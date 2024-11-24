using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FollowTargetController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [Header("Informational")]
    [SerializeField]
    private float distFromCenter;

    private void Start()
    {
        distFromCenter = transform.position.magnitude;
    }

    private void Update()
    {
        if (target != null)
            MoveTransformToTarget();
    }

    private void MoveTransformToTarget()
    {
        var up = target.position.normalized;
        var wp = up * distFromCenter;
        transform.position = wp;    

        transform.LookAt(target.position);
    }
}

