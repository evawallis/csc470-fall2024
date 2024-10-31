using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{

    public GameObject cameraObject;
    public Transform cameraTransform;

    public CharacterController cc; //reference to character controller

    float rotateSpeed = 50f;

    float moveSpeed = 12f;

    float jumpVelocity = 10f;

    float yVelocity = 0f; 
    float gravity = -9.8f;

    public Terrain terrain;
    public GameObject stump;
    public GameObject movingPlatform;
    public Vector3 previousMovingPlatformPosition;
    Vector3 amountPlatformMoved;
    bool onPlatform = false;

    float sampleTime;
    bool dash = false;

    float dashAmount = 8;
    float dashVelocity = 0;
    float friction = -2.8f;
    // float friction = -10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //get axes

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        //create camera position
        Vector3 flatCameraForward = cameraTransform.forward;
        flatCameraForward.y = 0;
        //player rotation
        transform.Rotate(0, rotateSpeed * hAxis * Time.deltaTime, 0);
        Vector3 amountToMove = flatCameraForward.normalized * moveSpeed * vAxis;
        amountToMove += cameraTransform.right * moveSpeed * hAxis;

        //gravity
        if (!cc.isGrounded)
        {
            if (yVelocity > 0 && Input.GetKeyUp(KeyCode.Space))//release space
            {
                yVelocity = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }

            yVelocity += gravity * Time.deltaTime;
        }
        else
        {
            yVelocity = -2;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }
        }
        //camera looks at player position
        // cameraTransform.position = new Vector3(transform.position.x, transform.position.y+5f, transform.position.z-5f);
        cameraTransform.LookAt(transform.position);
        //player movement
        cameraObject.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z - 20f);


        // if (movingPlatform != null)
        // {
        //     Vector3 amountPlatformMoved = movingPlatform.transform.position - previousMovingPlatformPosition;
        //     amountToMove += amountPlatformMoved;
        //     previousMovingPlatformPosition = movingPlatform.transform.position;
        // }

        // if (dash)
        // {
        //     if (Time.time >= sampleTime + 3)
        //     {
        //         amountToMove -= transform.forward * dashVelocity;
        //         bool dash=false;
        //     }
        //     else
        //     {
        //         amountToMove += transform.forward * dashVelocity;
        //     }
        // }

        // Update movement based on platform if player is on the platform
        if (onPlatform && movingPlatform != null)
        {
            Debug.Log("moving");
            amountPlatformMoved = movingPlatform.transform.position - previousMovingPlatformPosition;
            amountToMove += amountPlatformMoved;
            Debug.Log("moved");
            previousMovingPlatformPosition = movingPlatform.transform.position;
        }
        else
        {
            amountPlatformMoved = Vector3.zero; // Reset movement if player is off the platform
            previousMovingPlatformPosition = movingPlatform.transform.position;
        }
        
        // amountToMove += amountPlatformMoved;
        amountToMove.y += yVelocity; //adding velocity to part of position to make it move that far and that fast

        amountToMove *= Time.deltaTime;
        //player movement
        cc.Move(amountToMove);
        amountToMove.y = 0;
        transform.forward = amountToMove.normalized; //handle rotation
        // transform.rotation = Quaternion.LookRotation(amountToMove);

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     cc.Move(transform.forward);
        // }

        // float xPos = transform.position.x;
        // float yPos = transform.position.y;
        // float zPos = transform.position.z;
        // for (int count = 0; count < 20; count++)
        // {
        //     for (float i = xPos-50; i <= xPos+50; i++)
        //     {
        //         for (float j = zPos-50; j <= zPos+50; j++)
        //         {
        //             yPos = Terrain.activeTerrain.SampleHeight(transform.position);
        //             Instantiate(stump, new Vector3(xPos, yPos, zPos), Quaternion.identity);

        //         }
        //     }
        // }

        
    }
    void OnTriggerEnter(Collider other)
        {
            Debug.Log("triggered");

            if (other.CompareTag("stump"))
            {
                yVelocity = 25f;
                Debug.Log("stump");
            }
            // else if (other.CompareTag("mushroom"))
            // {
            //     // Dash();
            //     bool dash=true;
            //     sampleTime = Time.time;
            //     Debug.Log("mushroom");
            //     dashVelocity = dashAmount;
            //     dashVelocity += friction * Time.deltaTime;
            //     dashVelocity = Mathf.Clamp(dashVelocity, 0, 10000);

            // }
            else if (other.CompareTag("platform"))
            {
                Debug.Log("platform");
                // transform.position = other.transform.position;
                yVelocity = 0;
                onPlatform = true;
                // amountPlatformMoved = movingPlatform.transform.position - previousMovingPlatformPosition;
                previousMovingPlatformPosition = movingPlatform.transform.position;
                
            }
        }
    void Dash()
    {
        // float normalMove = moveSpeed;
        // float dashMove = moveSpeed * 2;
        // float startTime = Time.time;
        // moveSpeed = dashMove;
        // if (Time.time >= startTime + 3)
        // {
        //     moveSpeed = normalMove;

        // }
         
        // Slow the dash down, and keep it from going below zero (using clamp)
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("platform"))
        {
            onPlatform = false;
        }
    }
}
