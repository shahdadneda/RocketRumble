using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is Friendly");
                break;
            case "Finish":
                LevelUpDelay();
                break;
            case "Fuel":
                Debug.Log("You are fueling up");
                break;
            default:
                StartCrashSquence();
                break;

        }
    }

    float deathDelayTime = 1f;
    float levelDelayTime = 0.5f;

    void LevelUpDelay()
    {
        //Maybe add satisfying sound for level up in future?
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", levelDelayTime);
    }

    void StartCrashSquence()
    {
        //add sound for death and particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", deathDelayTime);

    }
   
    void loadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
