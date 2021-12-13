using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CourseLibrary
{

    public class EnemyManager

    {

        private List<EnemyController> deactivateEnemyList;        //variable list to store deactivate objects
        private Vector3[] enemySpawnPos = new Vector3[3];       //creating a array with 3 spawnPositions
        private GameObject enemyHolder;                         
        private float moveSpeed;                                //declaring a variable moveSpeed


        public EnemyManager(Vector3 spawnPos, float moveSpeed)     //constructor 
        {

            this.moveSpeed = moveSpeed;                         //setting the speed
            deactivateEnemyList = new List<EnemyController>();    // creating new object


            enemySpawnPos[0] = spawnPos - Vector3.right * 20;   //setting element at 0 spawn position
            enemySpawnPos[1] = spawnPos;                        //setting element at 1 spawn position
            enemySpawnPos[2] = spawnPos + Vector3.right * 20;   //setting element at 2 spawn position


            enemyHolder = new GameObject("EnemyHolder");       //creating new object 

          
        }


        public void SpawnEnemies(GameObject[] vehiclePrefabs)   //method
        {
            for(int i = 0; i < vehiclePrefabs.Length; i++)      //loop
            {

                GameObject enemy = Object.Instantiate(vehiclePrefabs[i], new Vector3(enemySpawnPos[1].x, 0, enemySpawnPos[1].z), Quaternion.identity); //spawning enemys
                enemy.SetActive(false);                                                                         //deactivate enemy                                    
                enemy.transform.SetParent(enemyHolder.transform);                                               //setting the parent
                enemy.name = "Enemy";                                                                           //setting the name      
                enemy.AddComponent<EnemyController>();
                enemy.GetComponent<EnemyController>().SetDefault(moveSpeed, this);
                deactivateEnemyList.Add(enemy.GetComponent<EnemyController>());                                   //adding to deactivate enemyList

            }

        }

      public void ActivateEnemy()       //methods
        {

            if(deactivateEnemyList.Count > 0) //if list is less than 0
            {

                EnemyController enemy = deactivateEnemyList[Random.Range(0, deactivateEnemyList.Count)];          //randomly getting enemy
                deactivateEnemyList.Remove(enemy);                                                             //removing enemy
                enemy.transform.position = enemySpawnPos[Random.Range(0, enemySpawnPos.Length)];             //setting spawn position
                enemy.ActivateEnemy();                                                                       //enemy activated
            } 

        }



        public void DeactivateEnemy(EnemyController enemy) //method
        {

            enemy.gameObject.SetActive(false);          //deactivate enemy
            deactivateEnemyList.Add(enemy);               //add to deactivate enemy
        }



    }
}