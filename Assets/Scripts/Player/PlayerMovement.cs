using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace SS {
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotateSpeed;
                
        private float distanceFromCenter;
        

        private void Start()
        {
            distanceFromCenter = transform.position.magnitude;            
        }


        private void Update()
        {
            var inputVec = Utils.InputUtils.GetInputVector();
            MovePlayerTransform(inputVec);
        }


        #region Sphere movement
        private void MovePlayerTransform(Vector3 moveDir)
        {
            // calculate the new position and clamp it to the sphere radius
            var new_position = transform.position + transform.forward * moveDir.y * moveSpeed* Time.deltaTime;
            new_position = new_position.normalized * distanceFromCenter;
            transform.position = new_position;

            // rotate             
            transform.Rotate(new Vector3(0, moveDir.x * rotateSpeed * Time.deltaTime, 0)); // rotate around Y axis

            // fix orientation of the unit
            var gravity_up = transform.position.normalized;
            var body_up = transform.up;
            Quaternion targetRotation = Quaternion.FromToRotation(body_up, gravity_up) * transform.rotation;
            transform.rotation = targetRotation;

        }
        #endregion Sphere movement


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position+transform.forward);
        }
    }

}