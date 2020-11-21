﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    [RequireComponent(typeof(WaypointNavigator))]
    public class BaseCreep : BaseUnit
    {
        public GameObject selected;
        public float speed = 2.0f;

        private WaypointNavigator waypointNavigator;

        public new void Awake()
        {
            base.Awake();

            eventMediator.AddOnUnitSelected(OnUnitSelected);
            eventMediator.AddOnUnitDeselected(OnUnitDeselected);
            eventMediator.AddOnUnitHovered(OnUnitHovered);

            waypointNavigator = GetComponent<WaypointNavigator>();
        }

        private void OnDestroy()
        {
            eventMediator.RemoveOnUnitSelected(OnUnitSelected);
            eventMediator.RemoveOnUnitDeselected(OnUnitDeselected);
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

        public void OnSpawn()
        {
            waypointNavigator.StartNavigation();
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

        private void OnUnitHovered(RaycastHit pHitInfo)
        {
            if (pHitInfo.transform.name == transform.name)
            {
                //Debug.Log(transform.name);
            }
        }
    }
}
