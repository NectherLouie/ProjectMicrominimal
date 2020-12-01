using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Warmth
{
    public class UIEventBoardMediator : MonoBehaviour
    {
        private UIEventBoard board;

        public void Init()
        {
            board = FindObjectOfType<UIEventBoard>();
            
            if (board == null)
            {
                throw new System.Exception("UIEventBoardMediator Failed");
            }
        }

        // GameObject Listeners
        public void AddOnBaseActivateClicked(Action OnBaseActivateClicked)
        {
            board.OnBaseActivateClicked += OnBaseActivateClicked;
        }

        public void RemoveOnBaseActivateClicked(Action OnBaseActivateClicked)
        {
            board.OnBaseActivateClicked -= OnBaseActivateClicked;
        }

        public void AddOnBaseDeactivateClicked(Action OnBaseDeactivateClicked)
        {
            board.OnBaseDeactivateClicked += OnBaseDeactivateClicked;
        }

        public void RemoveOnBaseDeactivateClicked(Action OnBaseDeactivateClicked)
        {
            board.OnBaseDeactivateClicked -= OnBaseDeactivateClicked;
        }

        // -- UI Listeners
        public void AddOnUnitBaseActivateComplete(Action OnUnitBaseActivateComplete)
        {
            board.OnUnitBaseActivateComplete += OnUnitBaseActivateComplete;
        }

        public void RemoveOnUnitBaseActivateComplete(Action OnUnitBaseActivateComplete)
        {
            board.OnUnitBaseActivateComplete -= OnUnitBaseActivateComplete;
        }

        public void CompleteUnitBaseActivation()
        {
            board.CompleteUnitBaseActivation();
        }

        public void AddOnUnitBaseDeactivateComplete(Action OnUnitBaseDeactivateComplete)
        {
            board.OnUnitBaseDeactivateComplete += OnUnitBaseDeactivateComplete;
        }

        public void RemoveOnUnitBaseDeactivateComplete(Action OnUnitBaseDeactivateComplete)
        {
            board.OnUnitBaseDeactivateComplete -= OnUnitBaseDeactivateComplete;
        }

        public void CompleteUnitBaseDeactivation()
        {
            board.CompleteUnitBaseDeactivation();
        }
    }
}
