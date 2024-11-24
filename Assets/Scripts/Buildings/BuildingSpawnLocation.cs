using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SS
{
    public class BuildingSpawnLocation : MonoBehaviour, IBuildingAbility
    {
        public void Execute(float ts)
        {
            
        }

        public void Activate()
        {
            var enemyManager = FindEnemyManager();
            if (enemyManager == null)
            {
                Debug.LogError("Could not find the enemy manager");
                return;
            }

            enemyManager.SetSpawnLocation(transform);
            this.enabled = false;
        }

        private EnemyManager FindEnemyManager()
        {            
            return FindObjectOfType<EnemyManager>(); 
        }

        public bool IsRepeatable()
        {
            return false;
        }
    }
}