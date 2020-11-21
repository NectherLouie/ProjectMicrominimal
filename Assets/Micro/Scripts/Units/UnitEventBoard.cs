using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace Micro
{
    public class UnitEventBoard : MonoBehaviour
    {
        public Action<RaycastHit> OnUnitSelected;
        public Action OnUnitDeselected;
        public Action<RaycastHit> OnUnitMoved;
        public Action<RaycastHit> OnUnitHovered;

        public LayerMask navMeshMask;
        public LayerMask unitMask;
        public bool unitSelectionActive;

        private float clickSelectRate = 0.25f;
        private float clickSelectRateReset = 0.25f;

        private void Update()
        {
            RaycastHit hit;
            Ray ray = CameraController.main.playCamera.ScreenPointToRay(Input.mousePosition);
            
            if (unitSelectionActive)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, navMeshMask))
                {
                    // Click rate delay
                    clickSelectRate -= Time.deltaTime;
                    if (clickSelectRate <= 0)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("UnitEventBoard " + hit.transform.name + " Moved");
                            clickSelectRate = clickSelectRateReset;

                            OnUnitMoved?.Invoke(hit);
                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("UnitEventBoard Deselected Unit");

                        clickSelectRate = clickSelectRateReset;

                        unitSelectionActive = false;

                        OnUnitDeselected?.Invoke();
                    }
                }
            }

            if (!unitSelectionActive)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitMask))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        unitSelectionActive = true;

                        Debug.Log("UnitEventBoard " + hit.transform.name + " Selected");
                        
                        OnUnitSelected?.Invoke(hit);
                    }                    
                }
            }

            // Hover checks
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitMask))
            {
                OnUnitHovered?.Invoke(hit);
            }
        }
    }
}
