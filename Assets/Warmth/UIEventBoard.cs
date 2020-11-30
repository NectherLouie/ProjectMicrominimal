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
        public Action OnBaseDeployClicked;

        public void ClickBaseDeploy()
        {
            OnBaseDeployClicked?.Invoke();
        }
    }
}
