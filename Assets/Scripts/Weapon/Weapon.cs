using SS.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class Weapon : MonoBehaviour
    {
        [Header("Config")]

        [SerializeField] private Transform bulletSpawnLocation;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float cooldown;
        [SerializeField] private Transform bulletContainer;

        private float lastShotTime;
        private void Start()
        {
            UpdateLastShotTime();

        }

        private void Update()
        {
            
            if (InputUtils.IsMouseButtonDown(0))
                TryShootBullet();

        }


        private void TryShootBullet()
        {
            if (!CanShoot())
                return;
                        
            CreateBullet(bulletPrefab, bulletSpawnLocation, bulletContainer);
            UpdateLastShotTime();
        }

        private void CreateBullet(GameObject prefab, Transform spawnLocation, Transform container)
        {
            var go = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation, bulletContainer);
            go.SetActive(true);
        }


        private void UpdateLastShotTime()
        {
            lastShotTime = Time.time;
        }
        private bool CanShoot()
        {
            return (Time.time - lastShotTime) >= cooldown;
        }
    }

}