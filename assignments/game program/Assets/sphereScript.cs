using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sphereScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public Rigidbody rb;

    int score = 0;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Score: " + score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.useGravity = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // destroy coin (cube)
        Destroy(other.gameObject);
        score ++;
        if (score >= 3)
        {
            scoreText.text = "You win!";
        }
        else
        {
            scoreText.text = "Score: " + score;

        }
    }

    // public void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("hit the ground!");
    // }
}
