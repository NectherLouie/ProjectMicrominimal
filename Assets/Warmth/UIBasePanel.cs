using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Micro;

namespace Warmth
{
    public class UIBasePanel : MonoBehaviour
    {
        [Serializable]
        public class Config
        {
            public Button deployButton;
        }

        public Config config = new Config();

        private UnitSelectionBoardMediator eventBoardMediator;

        private void Awake()
        {
            eventBoardMediator = GetComponent<UnitSelectionBoardMediator>();
            eventBoardMediator.Init();

            eventBoardMediator.AddOnUnitSelected(OnUnitSelected);
            eventBoardMediator.AddOnUnitDeselected(OnUnitDeselected);
        }

        private void OnUnitSelected(RaycastHit pHitInfo)
        {
            GetComponent<CanvasGroup>().DOFade(1.0f, 0.25f).OnComplete(OnSelectionComplete);
        }

        private void OnSelectionComplete()
        {
            config.deployButton.enabled = true;
        }

        private void OnUnitDeselected()
        {
            GetComponent<CanvasGroup>().DOFade(0.0f, 0.25f).OnComplete(OnDeselectionComplete);
        }

        private void OnDeselectionComplete()
        {
            config.deployButton.enabled = false;
        }
    }
}
