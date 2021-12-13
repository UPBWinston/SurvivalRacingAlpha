using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource run;
    public AudioSource collision;
    public AudioSource spawnEnemy;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void launchSound(string nameSound){
        switch (nameSound) {
            case "run":
                run.Play();
                break;
            case "collision":
                collision.Play();
                break;
            case "spawnEnemy":
                spawnEnemy.Play();
                break;

        }
    }

}
