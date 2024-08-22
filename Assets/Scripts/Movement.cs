using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float sideThrust = 60f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation(); 
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }

    }
    
    void ProcessRotation(){

        if (Input.GetKey(KeyCode.A)){
            ApplyRotation(sideThrust);
        }

        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-sideThrust);

            
        }
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * sideThrust * Time.deltaTime);
    }

}


