using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace SS
{

    public class BuildingManager : MonoBehaviour
    {
        public enum BuildingTypes {
            SpawnLocation,
            Wall
        }

        [SerializeField] private BuildingCollectionScriptableObject buildingList;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform buildingContainer;

        private List<IBuildingAbility> activeBuildings = new List<IBuildingAbility>();

        public List<BuildingScriptableObject> GetBuildingsSO {get { return buildingList.buildings; }}

        public void BuildingBought(BuildingScriptableObject building)
        {
            Debug.Log($"Creating building : {building.buildingLabel}");

            playerController.PlaceBuilding(CreateBuilding(building), building, () => { Debug.Log("Cancelling placement stuff"); },this);
        }

        private Transform CreateBuilding(BuildingScriptableObject building)
        {
            var b = Instantiate(building.buildingPrefab, buildingContainer);
            b.transform.position = Utils.InputUtils.GetMouseWorldPosition();
            
            return b.transform;
        }

        public void BuildingPlaced(Transform building, BuildingScriptableObject buildingSO)
        {
            if(building.TryGetComponent<IBuildingAbility>(out var buildingAbility))
            {
                buildingAbility.Activate();
                if (buildingAbility.IsRepeatable())
                {
                    activeBuildings.Add(buildingAbility);
                }

                // update the path finder graph

                return;
            }
            else
            {
                Debug.Log($"Building transform did not have a IBuildingAbility : {buildingSO.name}");
                Destroy(building);
            }
        }
    }

}