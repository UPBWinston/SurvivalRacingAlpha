using System;
using UnityEngine;
using CourseLibrary;

namespace CourseLibrary
{
    
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        private float swipeThreshold = 0.15f;           //Threshold to conform swipe
        private Vector2 startPos, endPos;               //vector2 to decide swipe dircetion
        private Vector2 difference;                     //get the difference between startPos and endPos
        private SwipeType swipe = SwipeType.NONE;       //save swipeType
        private float swipeTimeLimit = 0.25f;           //TimeLimit to conform swipe
        private float startTime, endTime;
        [HideInInspector]
        public bool gameOver = false;


        public Action<SwipeType> swipeCallback;         //trigger event

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !gameOver)                    //if mouse down
            {
                startPos = endPos = Input.mousePosition;        //setting startPosition and endPosition
                startTime = endTime = Time.time;                //setting startTime and endTime
            }

            if (Input.GetMouseButtonUp(0) && !gameOver)                      //if mouse up
            {
                endPos = Input.mousePosition;                   //setting endPosition
                endTime = Time.time;                            //setting endTime
                if (endTime - startTime <= swipeTimeLimit)      //difference on time between
                {
                    DetectSwipe();                              //calling methods if less then time
                }
            }
        }

        private void DetectSwipe()                              //method
        {
            swipe = SwipeType.NONE;
            difference = endPos - startPos;                    //difference
            if (difference.magnitude > swipeThreshold * Screen.width)   //check if magnitude is more than Threshold
            {
                if (difference.x > 0) //swipe right
                {
                    swipe = SwipeType.RIGHT;
                }
                else if (difference.x < 0) //swipe left
                {
                    swipe = SwipeType.LEFT;
                }
            }

            if (swipeCallback != null)
            {

                swipeCallback(swipe);        //event calling
            }
        }


    }
}
