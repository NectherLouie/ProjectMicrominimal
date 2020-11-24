using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Micro
{
    public class WaypointNavigator : MonoBehaviour
    {
        public Action<WaypointNode> OnNavigationStarted;
        public Action<WaypointNode> OnNavigationUpdated;
        
        public WaypointNodeScanner scanner;

        private WaypointNode currentNode;
        private bool hasNode = false;

        public void StartNavigation()
        {
            scanner.OnScannerEnter += OnScannerEnter;
        }

        public void DestroyNavigation()
        {
            scanner.OnScannerEnter -= OnScannerEnter;
        }

        public void UpdateNavigation()
        {
            currentNode = currentNode.GetNextNode();
            
            hasNode = currentNode != null;
            if (hasNode)
            {
                OnNavigationUpdated?.Invoke(currentNode);
            }
        }

        private void OnScannerEnter(Collider pCollider)
        {
            if (!hasNode)
            {
                currentNode = pCollider.transform.GetComponent<WaypointNode>();

                hasNode = true;

                OnNavigationStarted?.Invoke(currentNode);
            }
        }
    }
}
