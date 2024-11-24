using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class BuildingWall : MonoBehaviour, IBuildingAbility, IModifyPathGraph
    {

        public bool IsRepeatable()
        {
            return false;
        }

        public void Execute(float ts)
        {
            
        }

        public void Activate()
        {
            this.enabled = false;
        }
    }
}