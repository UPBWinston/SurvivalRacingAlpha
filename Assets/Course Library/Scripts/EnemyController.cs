using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CourseLibrary
{

    public class EnemyController : MonoBehaviour
    {
        public float moveSpeed;  //declaring a variable 
        private EnemyManager enemyManager; //declaring  a variable



        public void SetDefault(float speed, EnemyManager enemyManager) //method to set speed and enemyManager
        {

            moveSpeed = speed;
            this.enemyManager = enemyManager;

        }



        public void ActivateEnemy() //method to set the enemy to true
        {

            gameObject.SetActive(true);
        }



      private void Update() 
        {

            if (GameManager.singleton.gameStatus == GameStatus.PLAYING)
            {


                transform.Translate(-transform.forward * moveSpeed * 0.8f * Time.deltaTime); // object moves at speed


                if (transform.position.z <= -10) // if poz of z is less
                {

                    enemyManager.DeactivateEnemy(this);
                }


            }

        }


    }
}