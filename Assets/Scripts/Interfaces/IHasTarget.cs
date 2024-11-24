using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public interface IHasTarget
    {
        void SetTarget(Transform target_transform);
        Transform GetTraget();
    }
}