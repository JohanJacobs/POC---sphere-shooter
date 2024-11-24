using SS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SS
{
    /*
        Player controller shoul be state machine to not have all the update functions 
        from all the components.
     */
    public class PlayerController : MonoBehaviour
    {
        private float distanceFromCenter;
        
        // Placing Building
        [SerializeField] private bool isPlacingBuilding;
        private Transform buildingToPlace;
        private Action cancelCallback;
        private BuildingScriptableObject placeBuildingSO;
        private BuildingManager buildingManager;

        private void Awake()
        {
            isPlacingBuilding = false;
            distanceFromCenter = transform.position.magnitude;
        }

        public void Update()
        {
            if (isPlacingBuilding)
            {
                DoBuildingPlacement();
            }
        }

        #region Building Placement
        public void PlaceBuilding(Transform building, BuildingScriptableObject buildingSO, Action cancelCallbackFN,BuildingManager buildingManager)
        {
            isPlacingBuilding = true;
            buildingToPlace = building;
            cancelCallback = cancelCallbackFN;
            placeBuildingSO = buildingSO;
            this.buildingManager = buildingManager;
        }

        private void DoBuildingPlacement()
        {
            if (!isPlacingBuilding)
                return;

            if (InputUtils.IsKeyDown(KeyCode.Escape)) {
                cancelCallback();
                DisableBuildingPlacement();
            }
                        
            // Update position
            var mouseWorldPos = InputUtils.GetMouseWorldPosition();
            buildingToPlace.transform.position = mouseWorldPos;

            int turnRot = 0;
            if (Utils.InputUtils.IsKeyDown(KeyCode.E))
                turnRot = 1;
            else if (Utils.InputUtils.IsKeyDown(KeyCode.Q))
                turnRot = -1;

            // rorate the object
            buildingToPlace.Rotate(0, turnRot * Time.deltaTime * 100f, 0);
            
            // Fix the orientation up
            var gravity_up = buildingToPlace.position.normalized;
            var body_up = buildingToPlace.up;
            Quaternion targetRotation = Quaternion.FromToRotation(body_up, gravity_up) * buildingToPlace.rotation;
            buildingToPlace.rotation = targetRotation;

            // debug stuff
            //Debug.DrawLine(buildingToPlace.position, buildingToPlace.position + buildingToPlace.forward * 2, Color.blue, 0.1f);
            //Debug.DrawLine(buildingToPlace.position, buildingToPlace.position + buildingToPlace.up * 2, Color.green, 0.1f);
            //Debug.DrawLine(buildingToPlace.position, buildingToPlace.position + buildingToPlace.right * 2, Color.red, 0.1f);

            if (InputUtils.IsMouseButtonDown(0))
            {
                buildingManager.BuildingPlaced(buildingToPlace, placeBuildingSO);
                DisableBuildingPlacement();
            }
        }
        
        private void DisableBuildingPlacement()
        {
            isPlacingBuilding = false;
            buildingToPlace = null;
            cancelCallback = null;
            placeBuildingSO = null;
        }

        #endregion Building Placement
    }
}