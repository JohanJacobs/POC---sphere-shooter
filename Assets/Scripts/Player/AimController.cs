using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS {
    public class AimController : MonoBehaviour
    {
        [Header("World Config")]
        [SerializeField]
        private LayerMask worldLayer;

        [Header("Aim Point Visual")]
        [SerializeField]
        private Transform AimPoint;
        [SerializeField]
        [Range(0.5f, 1.5f)]
        private float aimPointDistance;

        [Header("informational")]
        [SerializeField]
        private Vector3 lastGoodAimDirection;


        private void Start()
        {
            lastGoodAimDirection = transform.forward;
        }

        private void Update()
        {
            var mouse_position = Utils.InputUtils.GetMouseScreenPos();
            
            var aim_direction = GetAimDirection(mouse_position); 
            
            PositionAimPoint(aim_direction);
        }

        private void PositionAimPoint(Vector3 aim_direction)
        {
            var gunOffset = (aim_direction * aimPointDistance) + (transform.up * 0.5f);

            AimPoint.transform.position = transform.position + gunOffset;
            AimPoint.transform.rotation = Quaternion.LookRotation(aim_direction, transform.up);
        }


        #region Input_logic_for_mouse
        /*
            Requires a screen point that is on the world sphere , 
            if an invalid screen pos is provided then negative infinity is returned.
         */

        private Vector3 GetAimDirection(Vector3 mouse_screen_position)
        {
            var sphere_position = GetSphereWorldPositionFromMouseScreenPosition(mouse_screen_position);
            
            if (sphere_position.x == float.NegativeInfinity)
            {
                return lastGoodAimDirection;
            }

            return Utils.CalcUtils.CalcualteForwardVectorForSphere(transform.position,sphere_position).normalized;
        }

        private Vector3 GetSphereWorldPositionFromMouseScreenPosition(Vector2 mouse_screen_position)
        {
            var ray = Camera.main.ScreenPointToRay(mouse_screen_position);
            
            if (Physics.Raycast(ray, out var raycastHit, 100f, worldLayer))
            {
                // hit the sphere, get the world point and return it 
                return raycastHit.point;
            }
            return Vector3.negativeInfinity;
        }
        #endregion Input_logic_for_mouse

    }
}
