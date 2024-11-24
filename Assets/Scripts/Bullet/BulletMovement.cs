using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private float distFromCenter;
    private void Start()
    {
        distFromCenter = transform.position.magnitude;
    }


    private void Update()
    {
        MoveBulletTransform();
    }

    private void MoveBulletTransform()
    {
        // update position
        var new_position = transform.position + transform.forward * speed * Time.deltaTime;
        new_position = new_position.normalized * distFromCenter;
        transform.position = new_position;

        // fix orientation of the unit
        var gravity_up = transform.position.normalized;
        var body_up = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(body_up, gravity_up) * transform.rotation;
        transform.rotation = targetRotation;
    }
}
