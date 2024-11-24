using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SS
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private float damageAmount;
        private float damageLeft;


        public class OnDamageDealthEventArgs : EventArgs {
            public int damageAmount;
        }
        
        public static EventHandler<OnDamageDealthEventArgs> onDamageDealthEvent;
        private void Start()
        {
            damageLeft = damageAmount;
        }

        private void OnTriggerEnter(Collider other)
        {
            TryDoDamage(other.transform);
        }

        private void TryDoDamage(Transform target)
        {
            if (target.TryGetComponent<HealthSystem>(out var hs))
            {
                DoDamage(hs);
                CheckBulletLife();
            }
        }
                
        private void DoDamage(HealthSystem hs)
        {
            var amount = hs.currentHealth < damageLeft ? hs.currentHealth : damageLeft;

            hs.Damage(amount);
            damageLeft -= amount;
            onDamageDealthEvent?.Invoke(this, new OnDamageDealthEventArgs
            {
                damageAmount = (int)amount
            });
        }

        private void CheckBulletLife()
        {
            if (damageLeft <= 0)
                Destroy(this.gameObject);
        }
    }
}