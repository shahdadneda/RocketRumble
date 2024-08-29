using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float sideThrust = 60f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip engineSound;
    private AudioSource audioSource; 
    Rigidbody rb;

    private CollisionHandler collisionHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        //print("in movment: has won: " + collisionHandler.hasWon + " has crashed " + collisionHandler.hasCrashed);

    }

    void ProcessThrust(){
        if(collisionHandler.hasWon  || collisionHandler.hasCrashed)
        {
            //audioSource.Stop();
            return;
        }

        if (Input.GetKey(KeyCode.Space) ) {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = engineSound;
                audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }

    }
    
    void ProcessRotation(){
        if (collisionHandler.hasWon == true || collisionHandler.hasCrashed == true)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A)){
            ApplyRotation(sideThrust);
        }

        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-sideThrust);
            
        }
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually
        transform.Rotate(Vector3.forward * rotationThisFrame * sideThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing it so that physics sytem takes over
    }

}


