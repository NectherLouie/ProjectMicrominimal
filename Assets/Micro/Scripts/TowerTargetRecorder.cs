using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class TowerTargetRecorder : MonoBehaviour
    {
        public float radius = 4.0f;
        public List<GameObject> targets = new List<GameObject>();

        private void Awake()
        {
            gameObject.GetComponent<SphereCollider>().radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<BaseCreep>() != null)
            {
                targets.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<BaseCreep>() != null)
            {
                int targetIndex = targets.IndexOf(other.gameObject);
                targets.RemoveAt(targetIndex);
            }
        }

        public GameObject FetchTarget()
        {
            if (targets.Count > 0)
            {
                return targets[0];
            }

            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
