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


    // Start is called before the first frame update
    void Start()
    {
        SetColor();
        GameObject gmObj = GameObject.Find("GameManagerObject"); //make local variable based on string name
        gameManager = gmObj.GetComponent<GameManager>(); //set script to the variable we made
        // int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        // UpdateCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        // gameManager.IsGameOver();
        neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        gameManager.HandleRules(neighborCount, xIndex, yIndex);
        SetColor();
        if(!hasLand)
        {
            if(!alive && hasHouse)
            {
                Destroy(house);
                Debug.Log("destroyed house");
                hasHouse = false;
                land = Instantiate(soilPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
                Debug.Log("made land");
                hasLand = true;
                alive = false;
            }
        } 
    }

    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
            gameManager.HandleRules(neighborCount, xIndex, yIndex);
            SetColor();
            if(!hasLand)
            {
                if(!alive && hasHouse)
                {
                    Destroy(house);
                    Debug.Log("destroyed house");
                    hasHouse = false;
                    land = Instantiate(soilPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
                    Debug.Log("made land");
                    hasLand = true;
                    alive = false;
                }
            } 
            yield return new WaitForSeconds(1f);
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
        if(!hasLand)
        {
            if (alive)
            {
                alive = false;
                HandleHouses();
            }   
            else
            {
                alive = true;
                HandleHouses();
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
