using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    [CreateAssetMenu(fileName = "BuildingCollection", menuName = "ScriptableObjects/BuildingCollectionSO", order = 1)]
    public class BuildingCollectionScriptableObject : ScriptableObject
    {
        public List<BuildingScriptableObject> buildings;        
    }
}