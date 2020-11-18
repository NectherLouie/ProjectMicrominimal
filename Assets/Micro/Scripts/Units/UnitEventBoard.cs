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
        public Action<RaycastHit> OnUnitMoved;
        public Action OnUnitDeselected;

        public LayerMask navMeshMask;
        public LayerMask unitMask;
        public bool unitSelectionActive;

        private float clickSelectRate = 0.25f;
        private float clickSelectRateReset = 0.25f;

        private void Update()
        {
            if (unitSelectionActive)
            {
                RaycastHit hit;
                Ray ray = CameraController.main.playCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, navMeshMask))
                {
                    // Click rate delay
                    clickSelectRate -= Time.deltaTime;
                    if (clickSelectRate <= 0)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Debug.Log("UnitsEventBoard " + hit.transform.name + " Moved");
                            clickSelectRate = clickSelectRateReset;

                            OnUnitMoved?.Invoke(hit);
                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("UnitsEventBoard Deselected Unit");

                        clickSelectRate = clickSelectRateReset;

                        unitSelectionActive = false;

                        OnUnitDeselected?.Invoke();
                    }
                }
            }

            if (!unitSelectionActive)
            {
                RaycastHit hit;
                Ray ray = CameraController.main.playCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitMask))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        unitSelectionActive = true;

                        Debug.Log("UnitsEventBoard " + hit.transform.name + " Selected");
                        
                        OnUnitSelected?.Invoke(hit);
                    }
                }
            }
        }
    }
}
