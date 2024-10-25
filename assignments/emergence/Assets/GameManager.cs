using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject rakeImage;
    public GameObject seedImage;
    SoilScript[,] grid;
    public GameObject soilPrefab;
    float spacing = 1.1f;
    int size = 10;
    float simTimer;
    float simRate = 3f;
    public bool needToUpdate = true;
    public TMP_Text screenText; 
    public GameObject backgroundImage;
    public bool plantOrPick = false;
    public int numOfClicks = 0;
    // Start is called before the first frame update
    void Start()
    {
        rakeImage.SetActive(false);
        seedImage.SetActive(false);
        screenText.text = "Welcome to the Garden!\n Press 'space' to evolve from a seedling -> sprout -> flower! Click a weed to remove it, or click a blank cell to plant a seed.\nCreate as many flowers as you can. Weeds prevent growth in surrounding cells.";
        simTimer = simRate;
        grid = new SoilScript[size,size]; //instantiate grid with size 10x10
        for (int x = 0; x < size; x++){
            for (int y = 0; y < size; y++){
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing; //so grid can be on the ground 
                GameObject soil = Instantiate(soilPrefab, pos, Quaternion.identity);//Quaternion.identity is no rotation
                grid[x,y] = soil.GetComponent<SoilScript>();
                // grid[x,y].alive = (Random.value > 0.5f); //random.value returns a value between 0 and 1, so if its greater itll be true (flipping a coin)
                if (Random.value > 0.5f)
                {
                    grid[x,y].MakeSeedling();
                }
                grid[x,y].xIndex = x;
                grid[x,y].yIndex = y;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("made wee");
            GenerateWeeds();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            screenText.text = "";
            Destroy(backgroundImage);
            rakeImage.SetActive(true);
            seedImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            plantOrPick = true;
            numOfClicks = 3;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            plantOrPick = false;
            numOfClicks = 1;
        }
        string isGameOver = GameOver();
        if (System.String.Equals(isGameOver, "win"))
        {
            screenText.text = "You win!\nMore than 25% of the garden is fully bloomed.";
        }
        else if (System.String.Equals(isGameOver, "lose"))
        {
            screenText.text = "You lose.\nThere are too many weeds to revive the garden.";
        }
        simTimer -= Time.deltaTime;
        if (simTimer < 0 && Input.GetKeyDown(KeyCode.Space) && needToUpdate)
        {
            Simulate();
            needToUpdate = false;
        }
    }

    void Simulate()
    {
        SoilScript[,] nextGen = new SoilScript[size,size];
        nextGen = grid;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                int seedlingNeighborCount = CountNeighbors(x, y, "seedling");
                int sproutNeighborCount = CountNeighbors(x, y, "sprout");
                HandleRules(x, y, seedlingNeighborCount, sproutNeighborCount);
            }
        }
        GenerateWeeds();
    }

    public int CountNeighbors(int xIndex, int yIndex, string type)
    {
        int count = 0;
        for (int x = xIndex - 1; x <= xIndex + 1; x++) //goes from 1 to left to 1 to right
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {

                try
                {
                    if (!(x== xIndex && y== yIndex))
                    {
                        if (System.String.Equals(type, "seedling"))
                        {
                            if (grid[x,y].hasSeedling)
                            {
                                count ++;
                            }
                        }
                        else if (System.String.Equals(type, "sprout"))
                        {
                            if (grid[x,y].hasSprout)
                            {
                                count ++;
                            }
                        }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    count += 0;
                } 
            }
        }
        return count;
    }

    public void GenerateWeeds()
    {
        int x = Random.Range(0, 10);
        int y = Random.Range(0, 10);
        while (grid[x,y].hasWeed && grid[x,y].hasFlower)
        {
            x = Random.Range(0, 10);
            y = Random.Range(0, 10);
        }
        grid[x,y].MakeWeed();
        KillNeighbors(x,y);
        return;



        // Debug.Log("generate");
        // if (!(grid[x,y].hasWeed))
        // {
        //     grid[x, y].MakeWeed();
        //     KillNeighbors(x, y);
        // }
        // return;
    }
    
    public void KillNeighbors(int xIndex, int yIndex)
    {
        Debug.Log("kill neighbors");
        for (int x = xIndex - 1; x <= xIndex + 1; x++)
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {
               try
                {
                    // if (!(x== xIndex && y== yIndex))
                    // {
                        grid[x,y].Kill();
                        Debug.Log("killed" + x + " " + y);
                    
                }
                catch (System.IndexOutOfRangeException)
                {
                    continue;
                } 
            }
        }
        return;
    }

    public void ReviveNeighbors()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                grid[x,y].alive = true;
                if (grid[x,y].hasWeed)
                {
                    for (int i = x - 1; i <= x+1; i++)
                    {
                        for (int j = y-1; j <= y+1; j++)
                        {
                            try
                            {
                                grid[i,j].alive = false;
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        return;
        
        // for (int x = xIndex - 1; x <= xIndex + 1; x++)
        // {
        //     for (int y = yIndex - 1; y <= yIndex + 1; y++)
        //     {
        //        try
        //         {
        //             grid[x,y].alive = true;
        //             Debug.Log("revived" + x + " " + y);
        //         }
        //         catch (System.IndexOutOfRangeException)
        //         {
        //             continue;
        //         } 
        //     }
        // }
        // return;
    }

    public void HandleRules(int x, int y, int seedlingNeighborCount, int sproutNeighborCount)
    {
        bool changed = false;
        if (grid[x,y].hasSeedling && seedlingNeighborCount >= 3)
        {
            grid[x,y].MakeSprout();
            changed = true;
        }
        else if (grid[x,y].hasSprout && sproutNeighborCount >= 6 && !(changed))
        {
            grid[x,y].MakeFlower();
        }
        return;
    }

    public string GameOver()
    {
        int flowerCount = 0;
        int weedCount = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x,y].hasFlower)
                {
                    flowerCount++;
                }
                else if (grid[x,y].hasWeed)
                {
                    weedCount++;
                }
            }
        }
        if (flowerCount >= 25)
        {
            return "win";
        }
        else if (weedCount >= 20)
        {
            return "lose";
        }
        else
        {
            return "";
        }
    }
}
