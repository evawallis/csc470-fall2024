using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{

    public Renderer cubeRenderer; //reference to cube

    public bool alive = false; //not alive by default

    public int xIndex = -1; //position of cell
    public int yIndex = -1;

    public Color aliveColor; 
    public Color deadColor;

    int neighborCount; 

    GameManager gameManager; 


    float simTimer;
    float simRate = 0.1f;

    bool didSomethingChange = true;


    // Start is called before the first frame update
    void Start()
    {
        simTimer = simRate;
        SetColor();
        GameObject gmObj = GameObject.Find("GameManagerObject"); //make local variable based on string name
        gameManager = gmObj.GetComponent<GameManager>(); //set script to the variable we made
        // int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        // UpdateCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        simTimer -= Time.deltaTime;
        if (simTimer < 0 && didSomethingChange == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                simTimer = simRate;
                neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
                didSomethingChange = gameManager.HandleRules(neighborCount, xIndex, yIndex);
                SetColor();
            }
        }
        else
        {
            Debug.Log("click something");
        }
    }

   

    void SetColor() //upon start, set color based on status of cell
    {
        if(alive)
        {
            cubeRenderer.material.color = aliveColor;
        }
        else
        {
            cubeRenderer.material.color = deadColor;
        }
    }

    void OnMouseDown()
    {
        if (alive)
        {
            alive = false;
            neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
            didSomethingChange = gameManager.HandleRules(neighborCount, xIndex, yIndex);
            SetColor();
        }   
        else
        {
            alive = true;
            neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
            didSomethingChange = gameManager.HandleRules(neighborCount, xIndex, yIndex);
            SetColor();
        }
    }
}
