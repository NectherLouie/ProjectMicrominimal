using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class BaseUnit : MonoBehaviour
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

        public LayerMask planeMask;
        public UnitState unitState = UnitState.IDLE;        
        public UnitSelectedState unitSelectedState = UnitSelectedState.DESELECTED;

        public float speed = 2.0f;

        private Vector3 moveTowardsPosition;
        private float clickToMoveDelay = 0.25f;
        private float clickToMoveDelayReset = 0.25f;

        public void OnMouseOver()
        {
            Debug.Log("OnMouseOver");
        }

        public void OnMouseDown()
        {
            if (unitSelectedState != UnitSelectedState.SELECTED)
            {
                unitSelectedState = UnitSelectedState.SELECTED;
            }
        }

        public void Update()
        {
            if (unitSelectedState == UnitSelectedState.SELECTED)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask))
                {
                    Transform objectHit = hit.transform;

                    Debug.Log(objectHit.name + ": " + hit.point.ToString());

                    // Delay before you can click to move
                    clickToMoveDelay -= Time.deltaTime;
                    if (clickToMoveDelay <= 0)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            moveTowardsPosition = new Vector3(hit.point.x, 0f, hit.point.z);
                            unitState = UnitState.MOVING;

                            clickToMoveDelay = clickToMoveDelayReset;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                unitSelectedState = UnitSelectedState.DESELECTED;
                clickToMoveDelay = clickToMoveDelayReset;
            }

            if (unitState == UnitState.MOVING)
            {
                transform.LookAt(moveTowardsPosition);
                transform.position = Vector3.MoveTowards(transform.position, moveTowardsPosition, speed * Time.deltaTime);
            }
        }
    }
}
