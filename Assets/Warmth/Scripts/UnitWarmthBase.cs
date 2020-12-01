using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using Micro;

namespace Warmth
{
    [RequireComponent(typeof(UIEventBoardMediator))]
    public class UnitWarmthBase : BaseUnit
    {
        public GameObject selected;

        [Serializable]
        public class Config
        {
            public GameObject radiusDisplay;
            public float radius = 10.0f;
        }

        public Config config = new Config();

        private UIEventBoardMediator uiEventMediator;

        public new void Awake()
        {
            base.Awake();

            eventMediator.AddOnUnitSelected(OnUnitSelected);
            eventMediator.AddOnUnitDeselected(OnUnitDeselected);
            eventMediator.AddOnUnitMoved(OnUnitMoved);
            eventMediator.AddOnUnitHovered(OnUnitHovered);

            uiEventMediator = GetComponent<UIEventBoardMediator>();
            uiEventMediator.Init();

            uiEventMediator.AddOnBaseActivateClicked(OnBaseActivateClicked);
            uiEventMediator.AddOnBaseDeactivateClicked(OnBaseDeactivateClicked);
        }

        private void OnDestroy()
        {
            eventMediator.RemoveOnUnitSelected(OnUnitSelected);
            eventMediator.RemoveOnUnitDeselected(OnUnitDeselected);
            eventMediator.RemoveOnUnitMoved(OnUnitMoved);
            eventMediator.RemoveOnUnitHovered(OnUnitHovered);

            uiEventMediator.RemoveOnBaseActivateClicked(OnBaseActivateClicked);
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

        private void OnBaseActivateClicked()
        {
            if (unitSelectedState == UnitSelectedState.SELECTED)
            {
                hasReachedDestination = true;
                navMeshAgent.isStopped = true;

                config.radiusDisplay.SetActive(true);
                config.radiusDisplay.transform.DOScale(new Vector3(config.radius, config.radius, 1.0f), 0.5f)
                    .OnComplete(OnBaseActivateAnimationComplete);
            }
        }

        private void OnBaseActivateAnimationComplete()
        {
            uiEventMediator.CompleteUnitBaseActivation();
        }

        private void OnBaseDeactivateClicked()
        {
            if (unitSelectedState == UnitSelectedState.SELECTED)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(transform.position);

                hasReachedDestination = false;

                config.radiusDisplay.SetActive(true);
                config.radiusDisplay.transform.DOScale(new Vector3(0, 0, 1.0f), 0.5f)
                    .OnComplete(OnBaseDeactivateAnimationComplete);
            }
        }

        private void OnBaseDeactivateAnimationComplete()
        {
            uiEventMediator.CompleteUnitBaseDeactivation();
        }
    }
}
