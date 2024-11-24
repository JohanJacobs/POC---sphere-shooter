using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform spawnLocationTransform;
        [SerializeField] private float distanceFromTop;

        [SerializeField] private Transform EnemyPrefab;
        [SerializeField] private float enemySpawnPerSecond;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private float fallSpeed;

        List<FallToWorld> fallingEnemies;
        
        [Header("Informational")]
        [SerializeField] private float nextSpawnTime;
        [SerializeField] private float spawnFrequency;

        private Vector3 spawnOffset;

        [SerializeField] bool SpawnActive=true;
        
        private void Start()
        {
            spawnFrequency = 1 / enemySpawnPerSecond;
            SetNextSpawnTime();
            fallingEnemies = new List<FallToWorld>();
        }

        private void Update()
        {
            //if (SpawnActive )
            {
                if (CanSpawn())
                    TrySpawnEnemy();
            }

            TickFallingObjects();
        }

        #region Falling objects
        // TODO: move falling logic to a new script
        private void TickFallingObjects()
        {
            List<FallToWorld> stillFalling = new List<FallToWorld>();
            List<FallToWorld> doneFalling = new List<FallToWorld>();

            // tick falling
            foreach (var f in fallingEnemies)
            {
                f.Tick(Time.deltaTime);

                if (f.isFalling)
                    stillFalling.Add(f);
                else
                    doneFalling.Add(f);
            }

            // save the enemies that is still falling 
            fallingEnemies.Clear();
            fallingEnemies = stillFalling;
                        

            // deal with enemies that stopped falling
            foreach(var f in doneFalling)
            {
                ActivateEnemy(f.enemy);
            }
        }
        #endregion Falling objects

        #region EnemyState
        private void DisableEnemy(Transform enemyTransform)
        {
            if (enemyTransform.TryGetComponent<EnemyController>(out EnemyController ec))
            {                
                ec.enabled = false;
            }

            if (enemyTransform.TryGetComponent<BoxCollider>(out var bc))
            {
                bc.enabled = false;
            }
        }
        private void ActivateEnemy(Transform enemyTransform)
        {
            if (enemyTransform.TryGetComponent<EnemyController>(out EnemyController ec))
            {
                ec.enabled = true;
                ec.SetTarget(playerTransform);
            }

            if (enemyTransform.TryGetComponent<BoxCollider>(out var bc))
            {
                bc.enabled = true;
            }            
        }
        #endregion EnemyState


        #region SpawnEnemy

        private void TrySpawnEnemy()
        {
            var spawn_location = spawnLocationTransform.position + distanceFromTop * spawnLocationTransform.up;

            // create the prefab
            var enemy = Instantiate(EnemyPrefab, spawn_location, spawnLocationTransform.rotation, enemyContainer);
            DisableEnemy(enemy.transform);

            // set the target to follow
            enemy.transform.rotation = spawnLocationTransform.rotation;

            // active falling script
            var ftw = new FallToWorld(enemy.transform, spawnLocationTransform.position, fallSpeed);
            fallingEnemies.Add(ftw);

            // update when we should spawn again
            SetNextSpawnTime();
            SpawnActive = false;
        }

        #endregion SpawnEnemy

        #region Spawn Timing
        // TODO: make a separate script to do the spawn timer as a callback
        private void SetNextSpawnTime()
        {
            nextSpawnTime = Time.time + spawnFrequency;
        }

        private bool CanSpawn()
        {
            return nextSpawnTime <= Time.time;
        }

        #endregion Spawn Timing

        #region Configuration Functions
        public void SetSpawnLocation(Transform newSpawnLocation)
        {
            if (
                newSpawnLocation == null || 
                newSpawnLocation == transform || 
                newSpawnLocation == spawnLocationTransform
                )
            {
                return;
            }

            spawnLocationTransform = newSpawnLocation;
        }
        #endregion Configuration Functions

    }
}