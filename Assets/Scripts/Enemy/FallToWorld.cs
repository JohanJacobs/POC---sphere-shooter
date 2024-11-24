using SS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS
{
    public class FallToWorld
    {
        public Transform enemy { get; private set; }
        Vector3 target;
        float fall_speed;
        public bool isFalling { get; private set; }
        public FallToWorld(Transform enemy, Vector3 target, float fall_speed)
        {
            this.target = target;
            this.enemy = enemy;
            this.fall_speed = fall_speed;
            this.isFalling = true;
        }

        public void Tick(float ts)
        {
            // vector to target
            var vec_to_target = (target - enemy.position);
            var norm = vec_to_target.normalized;
            var d = vec_to_target.magnitude;

            // move vector 
            var move_vec = norm * fall_speed * ts;

            if (move_vec.magnitude > d )
            {
                isFalling = false;
                enemy.transform.position = target;
            }
            else
            {
                enemy.transform.position = enemy.transform.position + move_vec;
            }
        }
    }
}