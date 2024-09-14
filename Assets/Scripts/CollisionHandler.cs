using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float deathDelayTime = 1f;
    float levelDelayTime = 1.5f;
    public bool hasCrashed = false;
    public bool hasWon = false;
    public bool crashingDisabled = false;

    private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip levelUpSound;

    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            loadNextLevel();
        }

        else if (Input.GetKey(KeyCode.C))
        {

            crashingDisabled = !crashingDisabled;
            print("C has be clicked!");

            //hasCrashed = false;
            //CancelInvoke("ReloadLevel");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (crashingDisabled) { return; }
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

    

 
    void LevelUpDelay()
    {
        if (!hasCrashed && !hasWon)
        {
            hasWon = true;
            //GetComponent<Movement>().enabled = false;
            Invoke("loadNextLevel", levelDelayTime);
            //audioSource.PlayOneShot(levelUpSound);
            audioSource.clip = levelUpSound;
            audioSource.loop = false;
            audioSource.Play();
            successParticles.Play();
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
            //audioSource.PlayOneShot(crashSound);
            crashParticles.Play();
        }
        
        

    }
   
    void loadNextLevel()
    {
        successParticles.Play();
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
