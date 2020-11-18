using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Micro
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController main;

        public Camera playCamera;

        private void Awake()
        {
            main = this;
        }
    }
}
