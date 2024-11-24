using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float startHealth;
        public float currentHealth { get; private set; }

        public class OnHealthAmountChangedEventArgs : EventArgs
        {
            public float healthAmount;            
        }

        public System.EventHandler<OnHealthAmountChangedEventArgs> onHealthChanged;
        private void Start()
        {
            currentHealth = startHealth;
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            NotifyHealthChanged(currentHealth);
        }

        public void Damage(float amount)
        {
            currentHealth -= amount;
            NotifyHealthChanged(currentHealth);
        }        

        private void NotifyHealthChanged(float currentHealth)
        {
            onHealthChanged?.Invoke(this, new OnHealthAmountChangedEventArgs { healthAmount = currentHealth });
        }

    }
}