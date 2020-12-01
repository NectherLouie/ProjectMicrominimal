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
            public Button activateButton;
            public Button deactivateButton;
        }

        public Config config = new Config();

        private UnitSelectionBoardMediator eventBoardMediator;
        private UIEventBoardMediator uiEventBoardMediator;

        private bool isBaseActive = false;

        private void Awake()
        {
            eventBoardMediator = GetComponent<UnitSelectionBoardMediator>();
            eventBoardMediator.Init();

            eventBoardMediator.AddOnUnitSelected(OnUnitSelected);
            eventBoardMediator.AddOnUnitDeselected(OnUnitDeselected);

            uiEventBoardMediator = GetComponent<UIEventBoardMediator>();
            uiEventBoardMediator.Init();

            uiEventBoardMediator.AddOnUnitBaseActivateComplete(OnUnitBaseActivateComplete);
            uiEventBoardMediator.AddOnUnitBaseDeactivateComplete(OnUnitBaseDeactivateComplete);

            config.activateButton.interactable = !isBaseActive;
            config.deactivateButton.interactable = !config.activateButton.interactable;
        }

        private void OnUnitSelected(RaycastHit pHitInfo)
        {
            GetComponent<CanvasGroup>().DOFade(1.0f, 0.25f).OnComplete(OnSelectionComplete);
        }

        private void OnSelectionComplete()
        {
            config.activateButton.interactable = !isBaseActive;
            config.deactivateButton.interactable = !config.activateButton.interactable;
        }

        private void OnUnitDeselected()
        {
            GetComponent<CanvasGroup>().DOFade(0.0f, 0.25f).OnComplete(OnDeselectionComplete);
        }

        private void OnDeselectionComplete()
        {
            config.activateButton.interactable = false;
            config.deactivateButton.interactable = false;
        }

        private void OnUnitBaseActivateComplete()
        {
            isBaseActive = true;

            config.activateButton.interactable = !isBaseActive;
            config.deactivateButton.interactable = !config.activateButton.interactable;
        }

        private void OnUnitBaseDeactivateComplete()
        {
            isBaseActive = false;

            config.activateButton.interactable = !isBaseActive;
            config.deactivateButton.interactable = !config.activateButton.interactable;

        }
    }
}
