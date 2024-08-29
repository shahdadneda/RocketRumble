using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //print("in collisison handler: has won: " + hasWon + " has crashed " + hasCrashed);

    }
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
            default:
                StartCrashSquence();
                break;

        }
    }

    float deathDelayTime = 1f;
    float levelDelayTime = 1.5f;
    public bool hasCrashed = false;
    public bool hasWon = false;
    private AudioSource audioSource; 
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip levelUpSound;

    void LevelUpDelay()
    {
        //Maybe add satisfying sound for level up in future?
        if (!hasCrashed && !hasWon)
        {
            hasWon = true;
            //GetComponent<Movement>().enabled = false;
            Invoke("loadNextLevel", levelDelayTime);
            //audioSource.PlayOneShot(levelUpSound);
            audioSource.clip = levelUpSound;
            audioSource.loop = false;
            audioSource.Play();
        }
       

           
    }

    void StartCrashSquence()
    {
        //add sound for death and particles
        //fix the sound issue where it continues playing
        if (!hasWon && !hasCrashed)
        {
            hasCrashed = true;
            //GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", deathDelayTime);
            audioSource.clip = crashSound;
            audioSource.loop = false;
            audioSource.Play();
            print("playing oof sound");
            //audioSource.PlayOneShot(crashSound);
        }
        
        

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
