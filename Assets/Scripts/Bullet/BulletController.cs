using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class BulletController : MonoBehaviour
    {
        public static System.EventHandler onBulletCreateEvent;
        public static System.EventHandler onBulletDestroyEvent;

        private void Start()
        {
            onBulletCreateEvent?.Invoke(this, new System.EventArgs());
        }
        private void OnDestroy()
        {
            onBulletDestroyEvent?.Invoke(this, new System.EventArgs());
        }
    }
}