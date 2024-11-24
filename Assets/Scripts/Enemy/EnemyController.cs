using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

namespace SS
{
    public class EnemyController : MonoBehaviour, IHasTarget
    {
        
        [Header("Configuration")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Transform target;

        private float distanceFromCenter;

        public static System.EventHandler OnEnemyCreated;
        public static System.EventHandler OnEnemyDestroyed;

        private void Start()
        {
            distanceFromCenter = transform.position.magnitude;
            OnEnemyCreated?.Invoke(this, new System.EventArgs());

        }
        private void OnDestroy()
        {
            OnEnemyDestroyed?.Invoke(this,new System.EventArgs());
        }

        private void Update()
        {
         
            if (target)            
                MoveTransform(GetMoveVectorToPlayer());
        }

        private Vector3 GetMoveVectorToPlayer()
        {
            return Utils.CalcUtils.CalcualteForwardVectorForSphere(transform.position, target.position);
        }

        private void MoveTransform(Vector3 moveDir)
        {
            var move_dir_vec = Utils.CalcUtils.CalcualteForwardVectorForSphere(transform.position, target.transform.position).normalized;

                        
            var move_vec = move_dir_vec * moveSpeed * Time.deltaTime;
            var new_pos = move_vec + transform.position;

            transform.position = new_pos;
            transform.up = transform.position.normalized;            
        }

        public void SetTarget(Transform target_transform)
        {
            target = target_transform;
        }

        public Transform GetTraget()
        {
            return target;
        }
    }


}