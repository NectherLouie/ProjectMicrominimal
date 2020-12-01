using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Micro;

namespace Warmth
{
    public class UIEventBoard : MonoBehaviour
    {
        public Action OnBaseActivateClicked;
        public Action OnBaseDeactivateClicked;
        public Action OnUnitBaseActivateComplete;
        public Action OnUnitBaseDeactivateComplete;

        public void ClickBaseActivate()
        {
            OnBaseActivateClicked?.Invoke();
        }

        public void ClickBaseDeactivate()
        {
            OnBaseDeactivateClicked?.Invoke();
        }

        public void CompleteUnitBaseActivation()
        {
            OnUnitBaseActivateComplete?.Invoke();
        }

        public void CompleteUnitBaseDeactivation()
        {
            OnUnitBaseDeactivateComplete?.Invoke();
        }
    }
}
