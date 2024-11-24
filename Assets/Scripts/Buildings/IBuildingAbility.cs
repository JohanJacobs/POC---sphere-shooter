using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public interface IBuildingAbility 
    {
        bool IsRepeatable();

        void Execute(float ts);
        void Activate();
    }
}