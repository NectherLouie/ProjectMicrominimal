﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class UnitEngineer : BaseUnit
    {
        public GameObject selected;

        public new void Awake()
        {
            base.Awake();

            eventMediator.AddOnUnitSelected(OnUnitSelected);
            eventMediator.AddOnUnitDeselected(OnUnitDeselected);
            eventMediator.AddOnUnitMoved(OnUnitMoved);
            eventMediator.AddOnUnitHovered(OnUnitHovered);
        }

        private void OnDestroy()
        {
            eventMediator.RemoveOnUnitSelected(OnUnitSelected);
            eventMediator.RemoveOnUnitDeselected(OnUnitDeselected);
            eventMediator.RemoveOnUnitMoved(OnUnitMoved);
            eventMediator.RemoveOnUnitHovered(OnUnitHovered);
        }

        public new void Update()
        {
            base.Update();

            if (hasReachedDestination)
            {
                hasReachedDestination = false;
            }
        }

        private void OnUnitSelected(RaycastHit pHitInfo)
        {
            if (pHitInfo.transform.name == transform.name)
            {
                if (unitSelectedState != UnitSelectedState.SELECTED)
                {
                    unitSelectedState = UnitSelectedState.SELECTED;

                    // Selection Visuals
                    selected.SetActive(true);
                }
            }
        }

        private void OnUnitDeselected()
        {
            unitSelectedState = UnitSelectedState.DESELECTED;

            // Selection Visuals
            selected.SetActive(false);
        }

        private void OnUnitMoved(RaycastHit pHitInfo)
        {
            if (unitSelectedState == UnitSelectedState.SELECTED)
            {
                unitState = UnitState.MOVING;

                navMeshAgent.SetDestination(pHitInfo.point);
            }
        }

        private void OnUnitHovered(RaycastHit pHitInfo)
        {
            if (pHitInfo.transform.name == transform.name)
            {
                //Debug.Log(transform.name);
            }
        }
    }
}
