using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SS
{
    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField]
        StatsManager statsManager;

        [Header("UI elements")]
        [SerializeField] private TextMeshProUGUI enemyKillsUI;
        [SerializeField] private TextMeshProUGUI enemyActiveUI;
        [SerializeField] private TextMeshProUGUI bulletCounterUI;
        [SerializeField] private TextMeshProUGUI damageDealtUI;

        private void Awake()
        {
            statsManager.onStatChangeEvent += StatsManager_OnStatChangedEvent;
        }

        private void Start()
        {
            UpdateEnemyRelatedStats();
            UpdateBulletRelatedStats();
        }

        private void OnDestroy()
        {
            statsManager.onStatChangeEvent -= StatsManager_OnStatChangedEvent;
        }

        private void UpdateEnemyRelatedStats()
        {
            enemyKillsUI.text  = $"Kills   : {statsManager.enemiesDestroyed}";
            enemyActiveUI.text = $"Active  : {statsManager.enemiesAlive}";            
        }
        private void UpdateBulletRelatedStats()
        {
            bulletCounterUI.text =  $"Bullets : {statsManager.bulletsActive}";
            damageDealtUI.text = $"#Damage : {statsManager.damageDealt}";            
        }
        private void StatsManager_OnStatChangedEvent(object sender, StatsManager.OnStatChangeEventArgs args)
        {
            switch (args.statType) {
                case StatsManager.StatType.EnemyCreated:                    
                case StatsManager.StatType.EnemyKilled:
                case StatsManager.StatType.Damage:
                    UpdateEnemyRelatedStats();
                    break;
                case StatsManager.StatType.BulletCreated:
                case StatsManager.StatType.BulletDestroyed:
                    UpdateBulletRelatedStats();
                    break;                    
            }
        }


    }
}