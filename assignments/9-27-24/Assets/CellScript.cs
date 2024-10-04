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

    bool hasHouse = false;
    bool hasLand = false;
    
    public GameObject housePrefab;
    public GameObject soilPrefab;

    GameManager gameManager; 

    GameObject house;
    GameObject land;

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
        // gameManager.IsGameOver();
        // neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        // gameManager.HandleRules(neighborCount, xIndex, yIndex);
        // SetColor();
        // if(!hasLand)
        // {
        //     if(!alive && hasHouse)
        //     {
        //         Destroy(house);
        //         Debug.Log("destroyed house");
        //         hasHouse = false;
        //         land = Instantiate(soilPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        //         Debug.Log("made land");
        //         hasLand = true;
        //         alive = false;
        //     }
        // } 
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
        if(!hasLand)
        {
            if (alive)
            {
                alive = false;
                neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
                gameManager.HandleRules(neighborCount, xIndex, yIndex);
                SetColor();
                didSomethingChange = true;

                // HandleHouses();
            }   
            else
            {
                alive = true;
                neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
                gameManager.HandleRules(neighborCount, xIndex, yIndex);
                SetColor();
                didSomethingChange = false;
                // HandleHouses();
            }
        }
        
    }
    void HandleHouses()
    {
        if(alive)
        {
            Vector3 pos = transform.position;
            pos.y += 1f;
            house = Instantiate(housePrefab, pos, Quaternion.identity);
            hasHouse=true;
            Debug.Log("made house from click");

        }
        else if (hasHouse)
        {
            Destroy(house);
            Debug.Log("destroyed house from click");
            hasHouse=false;
            land = Instantiate(soilPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
            Debug.Log("made land from click");
            hasLand = true;
            alive = false;
        }
    }
}
