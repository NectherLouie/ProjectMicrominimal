﻿using System.Collections;
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
        ACTION_0 = 2,
        ACTION_1 = 3,
    }

    public enum UnitSelectedState
    {
        DESELECTED = 0,
        SELECTED = 1
    }

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(UnitSelectionBoardMediator))]
    public class BaseUnit : MonoBehaviour
    {
        public UnitSelectedState unitSelectedState = UnitSelectedState.DESELECTED;
        public UnitState unitState = UnitState.IDLE;

        public bool hasReachedDestination = false;

        protected UnitSelectionBoardMediator eventMediator;
        protected NavMeshAgent navMeshAgent;

        public void Awake()
        {
            eventMediator = GetComponent<UnitSelectionBoardMediator>();
            eventMediator.Init();

            navMeshAgent = GetComponent<NavMeshAgent>();

            hasReachedDestination = false;
        }

        public void Update()
        {
            if (unitState == UnitState.MOVING)
            {
                if (!navMeshAgent.isStopped)
                {
                    hasReachedDestination = navMeshAgent.remainingDistance < 1.0f;
                }

                if (hasReachedDestination)
                {
                    unitState = UnitState.IDLE;
                }
            }
        }
    }
}
