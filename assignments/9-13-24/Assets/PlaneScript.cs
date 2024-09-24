using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject terrainObject;
    public TMP_Text screenText;
    public TMP_Text scoreText;

    float forwardSpeed = 0f;
    float xRotationSpeed = 60f;
    float yRotationSpeed = 60f;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {        
        screenText.text = "Rock Paper Scissors Shoot!\n\n Press space to start!";
        scoreText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
 
        

        if (Input.GetKeyDown(KeyCode.Space))//when user starts game
        {
            screenText.text = " ";
            forwardSpeed = 55f;
            scoreText.text = "Score: " + score;
        }
        float vAxis = Input.GetAxis("Vertical"); // is up or down pressed
        float hAxis = Input.GetAxis("Horizontal"); // is left or right pressed

        // if (vAxis < 0)
        // {
        //     forwardSpeed += -vAxis * 2;
        // }
        // if (vAxis > 0)
        // {
        //     forwardSpeed += -vAxis *2;
        // }
        //sets how much it should rotate
        Vector3 amountToRotate = new Vector3(0,0,0);

        amountToRotate.x = -vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed; 
        amountToRotate.z = -hAxis * yRotationSpeed;
        amountToRotate *= Time.deltaTime;
        // simplifies code for rotation by saving amounts in vector3
        transform.Rotate(amountToRotate, Space.Self);
        
        
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        
        

        //position camera
        Vector3 cameraPosition = transform.position;
        cameraPosition += - transform.forward * 50f;
        cameraPosition += Vector3.up * 16f;
        cameraObject.transform.position = cameraPosition;

        cameraObject.transform.LookAt(transform.position + transform.forward * 20f);
        // cameraObject.Rotate(0,0,0,Space.self);

        if (score == 24)
        {
            screenText.text = "You win!\n\nYou collected all the rocks!";
            forwardSpeed = 0f;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("collectible"))
        {
            Debug.Log(other.gameObject.name);

            Destroy(other.gameObject);
            score+=1;
            scoreText.text = "Score: " + score;
            forwardSpeed += 10f;
        }
        if (other.CompareTag("terrain"))
        {
            transform.position += new Vector3(0, 10, 0);
            forwardSpeed -= 5f;
        }
        if (other.CompareTag("killer"))
        {
            Destroy(this.gameObject);
            if (score > 1)
            {
                screenText.text = "Game Over\n\nYou collected " + score + " rocks!"; 
            }
            else
            {
                screenText.text = "Game Over\n\nYou collected " + score + " rock!";
            }
            
            scoreText.text = " ";
        }
        if (other.CompareTag("wall"))
        {
            transform.Rotate(0, 180, 0, Space.Self);
            forwardSpeed -= 5f;
        }
        
    }
    
}
