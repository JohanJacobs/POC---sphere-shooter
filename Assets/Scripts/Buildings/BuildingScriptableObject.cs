using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "ScriptableObjects/BuildingSO", order = 1)]
    public class BuildingScriptableObject : ScriptableObject
    {
        public BuildingManager.BuildingTypes buildingType;
        public GameObject buildingPrefab;
        public Sprite buildingIcon;
        public string buildingLabel;
    }
}