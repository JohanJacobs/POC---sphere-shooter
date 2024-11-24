using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    namespace Utils
    {
        public static class CalcUtils
        {
            public static Vector3 CalcualteForwardVectorForSphere(Vector3 fromPos, Vector3 endPos)
            {
                // vector orthogonal to the two vectors 
                var right = Vector3.Cross(fromPos, endPos).normalized;

                // forward vector from the enemy to the target
                var forward = Vector3.Cross(right, fromPos);

                return forward;

            }
        }
    }
}
