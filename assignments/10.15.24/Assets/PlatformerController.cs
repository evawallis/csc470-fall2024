using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    // Vector3 amountPlatformMoved;
    // bool onPlatform = false;

    float sampleTime;
    bool dash = false;

    float dashAmount = 16;
    float dashVelocity = 0;
    float friction = -2.8f;

    int appleCount = 0;

    public TMP_Text timer;
    public TMP_Text score;
    public TMP_Text main;

    float timeLeft = 1000;

    bool didGameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        main.text = "Collect all the apples\nand feed the horse \nbefore the timer runs out.\nPress Enter to start";
        
    }

    // Update is called once per frame
    void Update()
    {  
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            main.text = "";
            timeLeft = 120;
            didGameStart = true;
        }
      if (didGameStart)
      {  
        score.text = "Score: " + appleCount;
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            main.text = "Game Over!";
        }

        float minutes = Mathf.FloorToInt(timeLeft / 60);

        float seconds = Mathf.FloorToInt(timeLeft % 60);
        
        // if (seconds < 10)
        // {
        //     String secondText;
        //     secondText = "0" + seconds;
        // }
        // else 
        // {
        //     seconds.ToString();
        // }
        // seconds.ToString("D2");
        timer.text = $"{minutes}: {seconds:00}";


        }
        dashVelocity += friction * Time.deltaTime;
        dashVelocity = Mathf.Clamp(dashVelocity, 0, 10000);
         //get axes
        // Debug.Log("player position:");
        // Debug.Log(transform.position);
        // Debug.Log("platform position:");
        // Debug.Log(movingPlatform.transform.position);

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


    

        amountToMove += transform.forward * dashVelocity;
        amountToMove.y += yVelocity; //adding velocity to part of position to make it move that far and that fast

        amountToMove *= Time.deltaTime;

        // Update movement based on platform if player is on the platform
        if (movingPlatform != null)
        {
            // Debug.Log("player position:");
            // Debug.Log(transform.position);
            // Debug.Log("platform position:");
            // Debug.Log(movingPlatform.transform.position);
            // Debug.Log("moving");
            Vector3 amountPlatformMoved = movingPlatform.transform.position - previousMovingPlatformPosition;
            amountToMove += amountPlatformMoved;
            Debug.Log("moved");
            previousMovingPlatformPosition = movingPlatform.transform.position;
        }
        // else
        // {
        //     amountPlatformMoved = Vector3.zero; // Reset movement if player is off the platform
        //     previousMovingPlatformPosition = movingPlatform.transform.position;
        // }
        
        
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
            else if (other.CompareTag("mushroom"))
            {
                // Dash();
                // bool dash=true;
                // sampleTime = Time.time;
                Debug.Log("mushroom");
                dashVelocity = dashAmount;
                // dashVelocity += friction * Time.deltaTime;
                // dashVelocity = Mathf.Clamp(dashVelocity, 0, 10000);

            }
            else if (other.CompareTag("platform"))
            {
                Debug.Log("platform");
                // transform.position = other.transform.position;
                // yVelocity = 0;
                // onPlatform = true;
                // amountPlatformMoved = movingPlatform.transform.position - previousMovingPlatformPosition;
                // previousMovingPlatformPosition = movingPlatform.transform.position;
                movingPlatform = other.gameObject;
                previousMovingPlatformPosition = movingPlatform.transform.position;
            }
            else if (other.CompareTag("apple"))
            {
                Debug.Log("collected");
                appleCount += 1;
                Debug.Log("apple count: " + appleCount);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("horse"))
            {
                Debug.Log("horse");
                if (appleCount >= 6)
                {
                    main.text = "You win!";
                }
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
            movingPlatform = null;
        }
    }
}
