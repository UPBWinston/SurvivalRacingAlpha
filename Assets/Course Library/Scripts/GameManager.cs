using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CourseLibrary
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager singleton;

        [HideInInspector] public GameStatus gameStatus = GameStatus.NONE; // game status as NONE
        [HideInInspector] public int currentCarIndex = 0; // variable car index at 0


        private void Awake() // method
        {

            if(singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject); //dont destroy game object
            }
            else
            {

                Destroy(gameObject); //destroy game object


            }

        }


    }


}