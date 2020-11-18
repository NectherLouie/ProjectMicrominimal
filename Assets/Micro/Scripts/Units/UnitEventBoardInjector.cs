using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class UnitEventBoardInjector : MonoBehaviour
    {
        public UnitEventBoard board;
        public void Inject()
        {
            board = FindObjectOfType<UnitEventBoard>();
            
            if (board == null)
            {
                throw new System.Exception("Injection Failed");
            }
        }
    }
}
