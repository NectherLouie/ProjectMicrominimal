using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Micro
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SphereScanner<T> : MonoBehaviour
    {
        public Action<Collider> OnScannerEnter;
        public Action<Collider> OnScannerStay;

        public Vector3 center;
        public float radius;

        public void Awake()
        {
            Rigidbody _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;

            SphereCollider _sc = GetComponent<SphereCollider>();
            _sc.center = center;
            _sc.radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<T>() != null)
            {
                Debug.Log("Scanner: " + other.transform.name);
                OnScannerEnter?.Invoke(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.transform.GetComponent<T>() != null)
            {
                Debug.Log("Scanner: " + other.transform.name);
                OnScannerStay?.Invoke(other);
            }
        }
    }
}
