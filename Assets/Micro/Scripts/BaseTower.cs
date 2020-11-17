using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [RequireComponent(typeof (TowerTargetRecorder))]
    public class BaseTower : MonoBehaviour
    {
        public enum TowerState
        {
            INACTIVE = 0,
            SHOOTING = 1,
            TRANSITIONING = 2,
        }

        public TowerState towerState = TowerState.INACTIVE;

        public GameObject target;
        public float maxFireRate = 1.0f;
        public float fireRate = 0f;

        private void Update()
        {
            if (towerState == TowerState.SHOOTING)
            {
                target = GetComponent<TowerTargetRecorder>().FetchTarget();
                if (target != null)
                {
                    // Shoot
                    fireRate -= Time.deltaTime;
                    if (fireRate <= 0)
                    {
                        fireRate = maxFireRate;
                        Shoot();
                    }

                    // Look at target
                    TurnTowardsTarget();
                }
                else
                {
                    // Reset if no target
                    fireRate = maxFireRate;
                }
            }
        }

        public virtual void TransitionToShooting()
        {
            towerState = TowerState.TRANSITIONING;

            OnTransitionToShootingComplete();
        }

        public virtual void OnTransitionToShootingComplete()
        {
            towerState = TowerState.SHOOTING;
        }

        public virtual void TransitionToInactive()
        {
            towerState = TowerState.TRANSITIONING;

            OnTransitionToInactiveComplete();
        }

        public virtual void OnTransitionToInactiveComplete()
        {
            towerState = TowerState.INACTIVE;
        }

        public virtual void Shoot()
        {
            Debug.Log("Shoot! " + target.name);
        }

        public virtual void TurnTowardsTarget()
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10.0f * Time.deltaTime);
        }
    }
}
