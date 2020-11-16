using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [RequireComponent(typeof (TowerTargetRecorder))]
    public class BaseTower : MonoBehaviour
    {
        public GameObject target;
        public float maxFireRate = 1.0f;
        public float fireRate = 0f;

        private void Update()
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
