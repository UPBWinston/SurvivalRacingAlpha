
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace CourseLibrary
{

    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {

        private float endXPos = 0;
        private Rigidbody myBody;
        private new Collider collider;
        private InputManager inputManagerScript;


        private void OnDisable() //method
        {

            InputManager.instance.swipeCallback -= SwipeMethod;

        }


        void Start() // method
        {

            myBody = gameObject.GetComponent<Rigidbody>(); //rigidbody reference
            myBody.isKinematic = true;                     //isKinematic to true
            myBody.useGravity = false;                     //useGravity to false
            SpawnVehicle(GameManager.singleton.currentCarIndex);
            inputManagerScript = GameObject.FindObjectOfType<InputManager>();

        }


        public void GameStarted() //method
        {
            InputManager.instance.swipeCallback += SwipeMethod;

        }


        public void SpawnVehicle(int index) //method
        {
            if(transform.childCount > 0)
            {

                Destroy(transform.GetChild(0).gameObject);

            }

            
            GameObject child = Instantiate(LevelManager.instance.VehiclePrefabs[index], transform); // spawning first car at selected index
            collider = child.GetComponent<Collider>();  // collider reference
            collider.isTrigger = true;                  // triger set to true
        }



        void SwipeMethod(SwipeType swipeType)
        {

            switch (swipeType)
            {

                case SwipeType.RIGHT:                    // if right swipe
                    endXPos = transform.position.x + 23; // x position 23 right
                    break;
                case SwipeType.LEFT:                      // if left swipe
                    endXPos = transform.position.x - 23;  // x position -23 left
                    break;
               

            }

            endXPos = Mathf.Clamp(endXPos, -23, 23); // clamp  endX pos between -23 and 23
            transform.DOMoveX(endXPos, 0.15f);
        }

        private void OnTriggerEnter(Collider other) // method
        {

            if(other.GetComponent<EnemyController>())
            {

                if(GameManager.singleton.gameStatus == GameStatus.PLAYING)          // checking the gamestatus
                {
                    DOTween.Kill(this);                                           // killing the dotween 
                    LevelManager.instance.GameOver();
                    inputManagerScript.gameOver = true;
                    myBody.isKinematic = false;                                  // gravity set to false
                    myBody.useGravity = true;                                   // gravity set to true
                    myBody.AddForce(Random.insideUnitCircle.normalized * 100f); // adding a force
                    collider.isTrigger = false;

                }
            }

        }

    }

}