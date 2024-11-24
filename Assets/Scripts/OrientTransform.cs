using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SS
{
    public class OrientTransform : MonoBehaviour
    {
        private void Start()
        {
            OrientTransformToWorld();
        }

        public void OrientTransformToWorld()
        {
            transform.up = transform.position.normalized;
        }
    }
}