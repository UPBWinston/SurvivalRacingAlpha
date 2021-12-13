using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CourseLibrary
{

    public class LevelManager : MonoBehaviour
    {

        public static LevelManager instance;

        [SerializeField] private float moveSpeed;               // moveSpeed of game
        [SerializeField] private float moveSpeedIncrease;       // moveSpeed of game increase
        [SerializeField] private GameObject roadPrefab;         //roadPrefab references
        [SerializeField] private GameObject[] vehiclePrefabs;   // vehiclePrefabs references from a set of vehicles available

        private List<GameObject> roadList;                      // list to store the roads in game which spawn            
        private Vector3 nextRoadPos = Vector3.zero;             // nextRoad position
        private GameObject roadHolder;                          //variable to store the road position
        private PlayerController playerController;              //variable to store the player controller characteristics
        private int roadAtLastIndex, roadAtTopIndex;            //variables for first piece of the road and the last one
        private EnemyManager enemyManager;                      //variable
        private int score = 0;
        private int highScore = 0;
        public UIManager uiManagerScript;
        public List<EnemyController> allEnemies;


        public PlayerController PlayerController {
            
            get { 
                
                return playerController; 
            } 
        
        }

        public GameObject[] VehiclePrefabs { 
            
            get {

                return vehiclePrefabs;
                
            } 
        }




        private void Awake()    //method
        {
            if (instance == null)
            {

                instance = this;
            }
            else
            {

                Destroy(gameObject);
            }

        }


        private void Start()    //method
        {
            AudioManager.instance.launchSound("run");
            roadHolder = new GameObject("RoadHolder");  //creating a gameObject
            roadList = new List<GameObject>();


            for (int i = 0; i < 5; i++)        
            {

                GameObject road = Instantiate(roadPrefab, nextRoadPos, Quaternion.identity);// instantiate the road
                road.name = "Road" + i.ToString(); 
                road.transform.SetParent(roadHolder.transform);
                nextRoadPos += Vector3.forward * 10;            
                roadList.Add(road);                         //add road to roadList

            }

            enemyManager = new EnemyManager(nextRoadPos, moveSpeed); //creating a object

            SpawnPlayer(); //call method

            enemyManager.SpawnEnemies(vehiclePrefabs); // enemies spawn
        }


        private void SpawnPlayer()
        {

            GameObject player = new GameObject("Player");           // new object player 
            player.transform.position = Vector3.zero;               // position at 0
            player.AddComponent<PlayerController>();                // adding playerController script
            playerController = player.GetComponent<PlayerController>(); // playerController reference

        }


        public void GameStarted()
        {
            highScore = PlayerPrefs.GetInt("highScore", 0);
            uiManagerScript.ChangehighScore(highScore);
            GameManager.singleton.gameStatus = GameStatus.PLAYING; // game status to playing
            enemyManager.ActivateEnemy();                           // activate enemy
            playerController.GameStarted();                         //player game starting
            InvokeRepeating("SpawningEnemiesInterval", 2.0f, 1.0f);
            GameObject enemieHolder = GameObject.Find("EnemyHolder");
            foreach (Transform child in enemieHolder.transform)
            {
                allEnemies.Add(child.GetComponent<EnemyController>());
            }
            //AudioManager.instance.launchSound("run");

        }



        private void MoveRoad() // method to add road when player reaches a certain road position
        {
            for (int i = 0; i < roadList.Count; i++) // looping throught roadList
            {
                roadList[i].transform.Translate(-transform.forward * moveSpeed * Time.deltaTime); //each road move with speed
            }
            if(roadList[roadAtLastIndex].transform.position.z <= -10)   // z distance is at -10 if road is at 0 element
            {
                roadAtTopIndex = roadAtLastIndex - 1;

                if(roadAtTopIndex < 0) // if road less then 0
                {
                    roadAtTopIndex = roadList.Count - 1; // road list number - 1
                }
                roadList[roadAtLastIndex].transform.position = roadList[roadAtTopIndex].transform.position + Vector3.forward * 10; //change position of roads
                roadAtLastIndex++; // increase road by 1

                if (roadAtLastIndex >= roadList.Count)
                {
                    roadAtLastIndex = 0;
                }
            }
        }

        private void Update()
        {
            if (GameManager.singleton.gameStatus != GameStatus.FAILED) // checking if gamestatus is not failed
            {
                MoveRoad(); // calling method moveRoad
            }
        }

        private void SpawningEnemiesInterval()
        {
            enemyManager.ActivateEnemy();  // enemy activate
            AudioManager.instance.launchSound("spawnEnemy");
        }

        public void GameOver()
        {
            GameManager.singleton.gameStatus = GameStatus.FAILED;  // game status failed
            UIManager.instance.GameOver();
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
                uiManagerScript.ChangehighScore(highScore);
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.GetComponent<EnemyController>()) // if enemyController has collided object
            {

                score = score+1;
                moveSpeed = moveSpeed + moveSpeedIncrease;
                foreach(EnemyController enemie in allEnemies)
                {
                    enemie.moveSpeed = moveSpeed;
                }
                uiManagerScript.ChangeScore(score);
            }

        }
    }
}