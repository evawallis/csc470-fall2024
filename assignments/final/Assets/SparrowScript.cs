using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowScript : MonoBehaviour
{

    // public GameObject sparrow;


    public Animator anim; 

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

    Vector3 lastMovmentDirection;


    // Start is called before the first frame update
    void Start()
    {
        
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

        cameraTransform.LookAt(transform.position);
        //player movement
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z - 10f);

        amountToMove += transform.forward * dashVelocity;
        amountToMove.y += yVelocity; //adding velocity to part of position to make it move that far and that fast

        amountToMove *= Time.deltaTime;

        cc.Move(amountToMove);
        amountToMove.y = 0;
        transform.forward = amountToMove.normalized; //handle rotation
        


        
    }
}
