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

        public void AddOnBaseDeployClicked(Action OnBaseDeployClicked)
        {
            board.OnBaseDeployClicked += OnBaseDeployClicked;
        }

        public void RemoveOnBaseDeployClicked(Action OnBaseDeployClicked)
        {
            board.OnBaseDeployClicked -= OnBaseDeployClicked;
        }
    }
}
