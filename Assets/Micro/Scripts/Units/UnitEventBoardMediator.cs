using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Micro
{
    public class UnitEventBoardMediator : MonoBehaviour
    {
        private UnitEventBoard board;

        public void Init()
        {
            board = FindObjectOfType<UnitEventBoard>();
            
            if (board == null)
            {
                throw new System.Exception("UnitEventBoardMediator Failed");
            }
        }

        public void AddOnUnitSelected(Action<RaycastHit> OnUnitSelected)
        {
            board.OnUnitSelected += OnUnitSelected;
        }

        public void RemoveOnUnitSelected(Action<RaycastHit> OnUnitSelected)
        {
            board.OnUnitSelected -= OnUnitSelected;
        }

        public void AddOnUnitDeselected(Action OnUnitDeselected)
        {
            board.OnUnitDeselected += OnUnitDeselected;
        }

        public void RemoveOnUnitDeselected(Action OnUnitDeselected)
        {
            board.OnUnitDeselected -= OnUnitDeselected;
        }

        public void AddOnUnitMoved(Action<RaycastHit> OnUnitMoved)
        {
            board.OnUnitMoved += OnUnitMoved;
        }

        public void RemoveOnUnitMoved(Action<RaycastHit> OnUnitMoved)
        {
            board.OnUnitMoved -= OnUnitMoved;
        }

        public void AddOnUnitHovered(Action<RaycastHit> OnUnitHovered)
        {
            board.OnUnitHovered += OnUnitHovered;
        }

        public void RemoveOnUnitHovered(Action<RaycastHit> OnUnitHovered)
        {
            board.OnUnitHovered -= OnUnitHovered;
        }
    }
}
