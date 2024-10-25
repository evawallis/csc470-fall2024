using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilScript : MonoBehaviour
{

    public bool alive = true;

    public int xIndex = -1;
    public int yIndex = -1;

    public GameObject seedling;
    public GameObject sprout;
    public GameObject flower;
    public GameObject weed;

    GameObject seedlingObj;
    GameObject sproutObj;
    GameObject flowerObj;
    GameObject weedObj;

    public bool hasSeedling = false;
    public bool hasSprout = false;
    public bool hasFlower = false;
    public bool hasWeed = false;

    int neighborCount;

    bool updated = true;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gmObj = GameObject.Find("GameManagerObject");
        gameManager = gmObj.GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MakeSeedling()
    {
        Debug.Log("made seed");
        if (!(hasSprout && hasFlower && hasWeed))
        {
            seedlingObj = Instantiate(seedling, transform.position, Quaternion.identity);
            hasSeedling = true;
            alive = true;
        }
    }

    public void MakeSprout()
    {
        if (hasSeedling && !(hasFlower && hasWeed && alive))
        {
            sproutObj = Instantiate(sprout, transform.position, Quaternion.identity);
            hasSprout = true;
            alive = true;
            Destroy(seedlingObj);
            hasSeedling = false;
        }
    }

    public void MakeFlower()
    {
        if (hasSprout && !(hasSeedling && hasWeed && alive))
        {
            flowerObj = Instantiate(flower, transform.position, Quaternion.identity);
            hasFlower = true;
            alive = true;
            Destroy(sproutObj);
            hasSprout = false;
        }
        // return;
    }

    public void MakeWeed()
    {
        if (!(hasFlower))
        {
            weedObj = Instantiate(weed, transform.position, Quaternion.identity);
            hasWeed = true;
            alive = false;
            if (hasSeedling)
            {
                Destroy(seedlingObj);
                hasSeedling = false;
            }
            else if (hasSprout)
            {
                Destroy(sproutObj);
                hasSprout = false;
            }
        }
        return;
    }

    public void Kill()
    {
        alive = false;
        if (hasSeedling)
        {
            Destroy(seedlingObj);
            hasSeedling = false;
            alive = false;
        }
        else if (hasSprout)
        {
            Destroy(sproutObj);
            hasSprout = false;
            alive = false;
        }
    }

    void OnMouseDown()
    {
        if (!(hasSeedling && hasSprout && hasFlower && gameManager.needToUpdate)) //if its clickable
        {
            gameManager.needToUpdate = true;
            if (!gameManager.plantOrPick)//if picking
            {
                while (gameManager.numOfClicks > 0)
                {
                    if (hasWeed)
                    {
                        Debug.Log("tried to pick weed");
                        Destroy(weedObj);
                        hasWeed = false;
                        gameManager.ReviveNeighbors(); 
                        alive = true;
                        gameManager.numOfClicks--;
                    }
                }
            }
            else if (gameManager.plantOrPick)//if planting
            {
                while(gameManager.numOfClicks > 0)
                {
                    if (alive)
                    {
                        Debug.Log("tried to plant seed");
                        MakeSeedling();
                        gameManager.numOfClicks--;
                    }
                }
            }
        }







        // if (!(hasSeedling && hasSprout && hasFlower && gameManager.needToUpdate))
        // {
        //     gameManager.needToUpdate = true;
        //     if (hasWeed)
        //     {
        //         hasWeed = false;
        //         alive = true;
        //         Destroy(weedObj);
        //         Debug.Log("destroyed weed");
        //         gameManager.ReviveNeighbors();
        //     }
        //     else if (alive)
        //     {
        //         Debug.Log("attempted to make seed");
        //         MakeSeedling();
        //     }
        // }
        
    }

    
}
