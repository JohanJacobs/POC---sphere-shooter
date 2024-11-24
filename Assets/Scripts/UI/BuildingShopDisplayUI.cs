using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SS
{
    public class BuildingShopDisplayUI : MonoBehaviour
    {

        [SerializeField] private BuildingManager buildingManager;

        [Header("Configuration")]
        [SerializeField] private Transform tileTemplate;        
        [SerializeField] private int offsetY = 10;
        [SerializeField] private int gapX = 10;
        [SerializeField] private int tileWidth= 100;
        private void Awake()
        {
            if (buildingManager == null) {         
                var bm = FindObjectOfType<BuildingManager>();                
                if (bm == null)
                {
                    Debug.LogError("Could not locate building manager!");
                    return;
                }
                buildingManager = bm;
            }

            tileTemplate.gameObject.SetActive(false);
            CreateButtons(tileTemplate, buildingManager);
        }

        #region Setup Tiles

        void CreateButtons(Transform template, BuildingManager buildManager)
        {
            var buildingList = buildingManager.GetBuildingsSO;
            for(int i = 0; i < buildingList.Count; i++)
            {
                int x = gapX + i * (tileWidth+ gapX);
                int y = offsetY;
                var so = buildingList[i];
                var c = BuildingCard.CreateCard(
                        buildingList[i],
                        template, 
                        x,y,
                        ()=> { buildingManager.BuildingBought(so); }
                    );
            }
        }
        #endregion Setup Tiles
    }
}