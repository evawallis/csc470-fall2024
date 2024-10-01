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

    GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        SetColor();
        GameObject gmObj = GameObject.Find("GameManagerObject"); //make local variable based on string name
        gameManager = gmObj.GetComponent<GameManager>(); //set script to the variable we made
    }

    // Update is called once per frame
    void Update()
    {
        
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
        alive = !alive;
        SetColor();
        //count neighbors
        int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        Debug.Log("(" + xIndex + "," + yIndex + "): " + neighborCount);


    }
}
