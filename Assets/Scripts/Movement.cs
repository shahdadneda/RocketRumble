using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float sideThrust = 60f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip engineSound;
        //Particles for engines
    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftParticle;
    [SerializeField] ParticleSystem rightParticle;

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

    void Update()
    {
        // where we call our two movment functions, for side and main
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust(){
        if(collisionHandler.hasWon  || collisionHandler.hasCrashed)
        {
            //audioSource.Stop();
            return;
        }
        if (Input.GetKey(KeyCode.Space) ) {
            StartThrusting();
        }
        //stop everything if space is not being pressed
        else StopThrusting();




        void StartThrusting()
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = engineSound;
                audioSource.Play();
            }
            //check if particles arrent playing, then play particle
            if (!mainThrustParticle.isPlaying)
            {
                mainThrustParticle.Play();
            }
        }

    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }

    void ProcessRotation(){
        //only litsen to everything below if we have not won won or crashed, if we have then return
        if (collisionHandler.hasWon == true || collisionHandler.hasCrashed == true)
        {
            return;
        }

        //what to do when A is being pressed
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }

        //what to do when d is being pressed
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
    }



    private void RotateRight()
    {
        //same thing as 'a' rotation abo
        ApplyRotation(-sideThrust);
        if (!leftParticle.isPlaying)
        {
            leftParticle.Play();
        }
        else
        {
            leftParticle.Stop();
            print("left stoping");
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(sideThrust);

        //play right particle thrust if it is not already being played (its not)
        if (!rightParticle.isPlaying)
        {
            rightParticle.Play();
        }

        // if it is being played, then stop the particles
        else
        {
            rightParticle.Stop();
            print("right stoping");
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually
        transform.Rotate(Vector3.forward * rotationThisFrame * sideThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing it so that physics sytem takes over
    }

}


