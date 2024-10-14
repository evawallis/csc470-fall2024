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

    bool hasSeedling = false;
    bool hasSprout = false;
    bool hasFlower = false;
    bool hasWeed = false;

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
        if (Input.GetKey(KeyCode.Space))
        {
            updated = true;
            neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
            if (hasSeedling && neighborCount >= 3)
            {
                MakeSprout();
            }
            else if (hasSprout && neighborCount >=6)
            {
                MakeFlower();
            }
            // gameManager.GenerateWeeds(Random.Range(0, 10), Random.Range(0,10));
        }
    }

    public void MakeSeedling()
    {
        Debug.Log("made seed");
        if (!(hasSprout && hasFlower && hasWeed))
        {
            seedling = Instantiate(seedling, transform.position, Quaternion.identity);
            hasSeedling = true;
            alive = true;
        }
    }

    public void MakeSprout()
    {
        if (hasSeedling && !(hasFlower && hasWeed && alive))
        {
            sprout = Instantiate(sprout, transform.position, Quaternion.identity);
            hasSprout = true;
            alive = true;
            Destroy(seedling);
            hasSeedling = false;
        }
    }

    public void MakeFlower()
    {
        if (hasSprout && !(hasSeedling && hasWeed && alive))
        {
            flower = Instantiate(flower, transform.position, Quaternion.identity);
            hasFlower = true;
            alive = true;
            Destroy(sprout);
            hasSprout = false;
        }
        // return;
    }

    public void MakeWeed()
    {
        if (!(hasFlower))
        {
            weed = Instantiate(weed, transform.position, Quaternion.identity);
            hasWeed = true;
            alive = false;
            if (hasSeedling)
            {
                Destroy(seedling);
                hasSeedling = false;
            }
            else if (hasSprout)
            {
                Destroy(sprout);
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
            Destroy(seedling);
            hasSeedling = false;
            alive = false;
        }
        else if (hasSprout)
        {
            Destroy(sprout);
            hasSprout = false;
            alive = false;
        }
    }

    void OnMouseDown()
    {
        if (updated)
        {
            updated = false;
            if (!(hasSeedling && hasSprout && hasFlower))
            {
                if (hasWeed)
                {
                    hasWeed = false;
                    alive = true;
                    Destroy(weed);
                    gameManager.ReviveNeighbors(xIndex, yIndex);
                }
                else if (alive)
                {
                    Debug.Log("attempted to make seed");
                    MakeSeedling();
                }
            }
        }
        else
        {
            Debug.Log("update");
        }
    }

    
}
