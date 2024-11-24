using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SS
{
    public class StatsManager : MonoBehaviour
    {
        public int enemiesCreated { get; private set; }
        public int enemiesDestroyed { get; private set; }
        public int enemiesAlive{ get { return enemiesCreated - enemiesDestroyed; } }

        public int bulletsCreated { get;private set; }
        public int bulletsDestroyed { get; private set; }
        public int bulletsActive { get { return bulletsCreated - bulletsDestroyed; } }
        public int damageDealt { get; private set; }

        public enum StatType
        {
            EnemyKilled,
            EnemyCreated,
            BulletCreated,
            BulletDestroyed,
            Damage
        }

        public class OnStatChangeEventArgs : EventArgs {
            public StatType statType;            
        }

        public EventHandler<OnStatChangeEventArgs> onStatChangeEvent;

        private void Awake()
        {
            enemiesCreated = 0;
            enemiesDestroyed = 0;
            EnemyController.OnEnemyCreated += EnemyController_OnEnemyCreated;
            EnemyController.OnEnemyDestroyed += EnemyController_OnEnemyDestroyed;

            bulletsCreated = 0;
            bulletsDestroyed = 0;
            BulletController.onBulletCreateEvent += BulletController_OnBulletCreateEvent;
            BulletController.onBulletDestroyEvent += BulletController_OnBulletDestroyEvent;

            damageDealt = 0;
            DamageSystem.onDamageDealthEvent += DamageSystem_onDamageDealthEvent;

        }

        private void OnDestroy()
        {
            EnemyController.OnEnemyCreated -= EnemyController_OnEnemyCreated;
            EnemyController.OnEnemyDestroyed -= EnemyController_OnEnemyDestroyed;

            BulletController.onBulletCreateEvent -= BulletController_OnBulletCreateEvent;
            BulletController.onBulletDestroyEvent -= BulletController_OnBulletDestroyEvent;

            DamageSystem.onDamageDealthEvent -= DamageSystem_onDamageDealthEvent;
        }

        #region Event Functions
        private void EnemyController_OnEnemyCreated(object sender, System.EventArgs e)
        {
            enemiesCreated += 1;
            onStatChangeEvent?.Invoke(this,new OnStatChangeEventArgs
            {
                statType = StatType.EnemyCreated                
            });
        }
        private void EnemyController_OnEnemyDestroyed(object sender, System.EventArgs e) 
        {
            enemiesDestroyed += 1;
            onStatChangeEvent?.Invoke(this, new OnStatChangeEventArgs
            {
                statType = StatType.EnemyKilled
            });
        }

        private void BulletController_OnBulletCreateEvent(object sender, System.EventArgs e)
        {
            bulletsCreated += 1;
            onStatChangeEvent?.Invoke(this, new OnStatChangeEventArgs
            {
                statType = StatType.BulletCreated
            });
        }
        private void BulletController_OnBulletDestroyEvent(object sender, System.EventArgs e)
        {
            bulletsDestroyed += 1;
            onStatChangeEvent?.Invoke(this, new OnStatChangeEventArgs
            {
                statType = StatType.BulletDestroyed
            });
        }

        private void DamageSystem_onDamageDealthEvent(object sender, DamageSystem.OnDamageDealthEventArgs e)
        {
            damageDealt += e.damageAmount;
        }
        #endregion Event Functions


    }
}