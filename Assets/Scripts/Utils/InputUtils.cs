using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SS
{
    namespace Utils
    {
        public static class InputUtils 
        {
            public static Vector2 GetInputVector()
            {
                Vector2 dirVec = Vector2.zero;

                if (Input.GetKey(KeyCode.W))
                {
                    dirVec.y = 1;
                }
                else if (Input.GetKey(KeyCode.S)) 
                {
                    dirVec.y = -1;
                }


                if (Input.GetKey(KeyCode.A))
                {
                    dirVec.x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    dirVec.x = 1;
                }

                return dirVec;
            }
            public static Vector3 GetMouseScreenPos()
            {
                return Input.mousePosition;
            }

            public static Vector3 GetMouseWorldPosition()
            {
                var screenPos = GetMouseScreenPos();    
                var ray = Camera.main.ScreenPointToRay(screenPos);
                var mask = LayerMask.GetMask("World");
                if (Physics.Raycast(ray, out var hitInfo, 100f, mask))
                {
                    return hitInfo.point;
                }
                return Vector3.zero;
            }

            public static bool IsMouseButtonDown(int button)
            {
                return Input.GetMouseButton(0) && ! IsMouseOverUI();
            }
            public static bool IsMouseOverUI()
            {
                return EventSystem.current.IsPointerOverGameObject();
            }

            public static bool IsKeyDown(KeyCode keyCode) {
                return Input.GetKey(keyCode);
            }
        }

    }
}