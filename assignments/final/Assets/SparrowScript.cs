using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SparrowScript : MonoBehaviour
{



    public Animator squidAnim; 

    public Animator squidAnimInBox;

    public GameObject squidTextBox;

    public TMP_Text squidText;

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

    // bool isPlayerGrounded = true;

    Vector3 lastMovementDirection = Vector3.forward;

    int numSeeds = 0;

    public GameObject questionButton;
    public GameObject instructionsBox;

    public TMP_Text numSeedsText;

    public Animator wormAnim;

    public GameObject mapView;
    public GameObject mapButtonObj;


    // Start is called before the first frame update
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GameplayScene") // Replace "YourSceneName" with the actual name of your scene
        {
            canvas.SetActive(true);
            questionButton.SetActive(true);
            camera.SetActive(true);
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
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z - 10f);

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
        
        if (Input.GetKey(KeyCode.E))
        {
            anim.Play("Eat");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("worm"))
        {
            wormAnim.Play("Fear");
            wormAnim.Play("Eyes_Trauma");
        }
        if (other.CompareTag("squid"))
        {
            if (numSeeds < 20)
            {
                squidText.text = "I'll help you find the worm but it'll cost you. Bring me 20 seeds.";
                squidAnimInBox.Play("Eyes_Blink");
                squidTextBox.SetActive(true);
            }
            else
            {
                squidAnimInBox.SetTrigger("Eyes_Happy");
                squidText.text = "Thanks! Here's a token of my appreciation. Good luck and goodnight.";
                squidTextBox.SetActive(true);
                numSeeds-=20;
                numSeedsText.text = numSeeds.ToString();
                mapButtonObj.SetActive(true);
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("worm"))
        {
            wormAnim.Play("Idle_A");
            wormAnim.Play("Eyes_Blink");
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
                numSeedsText.text = numSeeds.ToString();
                Destroy(other.gameObject);
            }
       
        }
        if (other.CompareTag("worm"))
        {
            Debug.Log("worm collide");
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("worm eaten");
                wormAnim.SetTrigger("Die");
                wormAnim.Play("Eyes_Dead");
                Debug.Log("ending scene");
                SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
 

            }
        }
        
    }

    public void ShowInstructions()
    {
        instructionsBox.SetActive(true); 
        questionButton.SetActive(false);
        Debug.Log("ShowInstructions");
    }

    public void CloseInstructions()
    {
        instructionsBox.SetActive(false);
        questionButton.SetActive(true);
    }

    public void CloseSquidDialogue()
    {
        squidTextBox.SetActive(false);
        squidAnim.Play("Sleep");
    }

    public void OpenMapView()
    {
        mapView.SetActive(true);
    }

    public void CloseMapView()
    {
        mapView.SetActive(false);
    }

}
