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
    [RequireComponent(typeof(UnitEventBoardMediator))]
    public class BaseUnit : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;
        
        protected UnitEventBoardMediator eventMediator;

        public void Awake()
        {
            eventMediator = GetComponent<UnitEventBoardMediator>();
            eventMediator.Init();

            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}
