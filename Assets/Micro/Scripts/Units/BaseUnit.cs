using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Micro
{
    public enum UnitState
    {
        IDLE = 0,
        MOVING = 1,
        ACTION_0 = 2
    }

    public enum UnitSelectedState
    {
        DESELECTED = 0,
        SELECTED = 1
    }

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(UnitEventBoardInjector))]
    public class BaseUnit : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public GameObject selected;

        public UnitSelectedState unitSelectedState = UnitSelectedState.DESELECTED;

        private UnitEventBoardInjector unitEventBoardInjector;

        public void Awake()
        {
            unitEventBoardInjector = GetComponent<UnitEventBoardInjector>();
            unitEventBoardInjector.Inject();

            unitEventBoardInjector.board.OnUnitSelected += OnUnitSelected;
            unitEventBoardInjector.board.OnUnitDeselected += OnUnitDeselected;
            unitEventBoardInjector.board.OnUnitMoved += OnUnitMoved;
        }

        private void OnDestroy()
        {
            unitEventBoardInjector.board.OnUnitSelected -= OnUnitSelected;
            unitEventBoardInjector.board.OnUnitDeselected -= OnUnitDeselected;
            unitEventBoardInjector.board.OnUnitMoved -= OnUnitMoved;
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
                navMeshAgent.SetDestination(pHitInfo.point);
            }
        }
    }
}
