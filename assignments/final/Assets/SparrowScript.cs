using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SparrowScript : MonoBehaviour
{

    // public GameObject sparrow;


    public Animator anim; 

    public GameObject canvas;

    public CharacterController cc;

    public GameObject camera; 

    public Transform cameraTransform;

    float rotateSpeed = 5f;

    float moveSpeed = 12f;

    float jumpVelocity = 10f;

    float yVelocity = 0f; 

    float dashVelocity = 0f;
    float friction = -2.8f;

    float gravity = -9.8f;

    bool isPlayerGrounded = true;

    Vector3 lastMovementDirection = Vector3.forward;

    int numSeeds = 0;


    // Start is called before the first frame update
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GameplayScene") // Replace "YourSceneName" with the actual name of your scene
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // camera.transform.LookAt(transform.position);

        // camera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z + 10);
        // // camera.transform.position.y = transform.position.y + 4;

        dashVelocity += friction * Time.deltaTime;
        dashVelocity = Mathf.Clamp(dashVelocity, 0, 10000);

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");


        if (hAxis != 0 || vAxis != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        


        //create camera position
        Vector3 flatCameraForward = cameraTransform.forward;
        flatCameraForward.y = 0;
        //player rotation
        transform.Rotate(0, rotateSpeed * hAxis * Time.deltaTime, 0);
        Vector3 amountToMove = flatCameraForward.normalized * moveSpeed * vAxis;
        amountToMove += cameraTransform.right * moveSpeed * hAxis;


        if (!cc.isGrounded)
        {
            anim.SetBool("isJumping", true);
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
            anim.SetBool("isJumping", false);
            yVelocity = -2;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }
        }

        cameraTransform.LookAt(transform.position);
        //player movement
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z - 10f);

        amountToMove += transform.forward * dashVelocity;
        amountToMove.y += yVelocity; //adding velocity to part of position to make it move that far and that fast

        amountToMove *= Time.deltaTime;

        cc.Move(amountToMove);
        amountToMove.y = 0;


        if (amountToMove.magnitude > 0.01f) // Use sqrMagnitude for better performance
        {
            lastMovementDirection = new Vector3(amountToMove.x, 0, amountToMove.z).normalized; // Store last valid direction
            transform.forward = lastMovementDirection; // Update character's forward direction
        }


        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("seed"))
        {
            Debug.Log("seed collide");
            // Eat(other);
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("e pressed");
                anim.Play("Eat");
                numSeeds ++;
                Destroy(other.gameObject);
            }
       
        }
    }

}
