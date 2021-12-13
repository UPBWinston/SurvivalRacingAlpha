using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


namespace CourseLibrary
{
    
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        // references for holders and panels
        [SerializeField] private GameObject mainMenuPanel, gameMenuPanel, gameOverPanel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;


        private void Awake() // method
        {

            if(instance == null)
            {

                instance = this;
            }
        }

     
        public void PlayButton() //method
        {

            mainMenuPanel.SetActive(false);  // deactivating mainMenu
            gameMenuPanel.SetActive(true);  // activating gameMenu
            LevelManager.instance.GameStarted();

        }

        public void RetryButton() //method
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //scene reloading
        }

        public void GameOver() //method
        {
            AudioManager.instance.launchSound("collision");
            gameOverPanel.SetActive(true); //activating gameOverPanel
            

        }

        public void ChangeScore(int score) {
            scoreText.text = "Score : " + score;
        }

        public void ChangehighScore(int score)
        {
            highScoreText.text = "HighScore : " + score;
        }


    }

}