using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class BaseCreep : MonoBehaviour
    {
        public Transform target;
        public float speed = 2.0f;

        private void Update()
        {
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
