using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    CellScript[,] grid; //reference to a 2d array of cell scripts
    // can also do array of cell objects where cell has refernece to cell script

    public GameObject cellPrefab;
    

    float spacing = 1.1f;
    int size = 10;
    // Start is called before the first frame update
    void Start()
    {
        grid = new CellScript[size,size]; //instantiate grid with size 10x10
        for (int x = 0; x < size; x++){
            for (int y = 0; y < size; y++){
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing; //so grid can be on the ground 
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);//Quaternion.identity is no rotation
                grid[x,y] = cell.GetComponent<CellScript>();
                grid[x,y].alive = (Random.value > 0.5f); //random.value returns a value between 0 and 1, so if its greater itll be true (flipping a coin)
                grid[x,y].xIndex = x;
                grid[x,y].yIndex = y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // //from class
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     //evolve grid
        //     Simulate();
        // }
    }
    //from class
    


    public int CountNeighbors(int xIndex, int yIndex)
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
                        if (grid[x,y].alive)//if its not ourself and its alive
                        {
                            count++;
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

    public bool HandleRules(int numAliveNeighbors, int x, int y)
    {
        bool didSomethingChange;
        if (grid[x,y].alive)
        {
            if (numAliveNeighbors < 2)
            {
                grid[x,y].alive = false;
                // grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, 
                // grid[x,y].gameObject.transform.localScale.y - .5f,
				// grid[x,y].gameObject.transform.localScale.z);
                didSomethingChange = true;
            }
            else if (numAliveNeighbors > 3)
            {
                grid[x,y].alive = false;
                // grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, 
                // grid[x,y].gameObject.transform.localScale.y - .5f,
				// grid[x,y].gameObject.transform.localScale.z);
                didSomethingChange = true;
            }
            else 
            {
                grid[x,y].alive = true;
                // grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, 
                // grid[x,y].gameObject.transform.localScale.y + .5f,
				// grid[x,y].gameObject.transform.localScale.z);
                didSomethingChange = false;
            }
        }
        else
        {
            if (numAliveNeighbors == 3)
            {
                grid[x,y].alive = true;
                // grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, 
                // grid[x,y].gameObject.transform.localScale.y + .5f,
				// grid[x,y].gameObject.transform.localScale.z);
                didSomethingChange = true;
            }
            else
            {
                grid[x,y].alive = false;
                // grid[x,y].gameObject.transform.localScale = new Vector3(grid[x,y].gameObject.transform.localScale.x, 
                // grid[x,y].gameObject.transform.localScale.y - .5f,
				// grid[x,y].gameObject.transform.localScale.z);
                didSomethingChange = false;
            }
        }
        return didSomethingChange;
    }
    // public void IsGameOver()
    // {
    //     bool isGameOver = false;
    //     for (int x = 0; x < size; x++)
    //     {
    //         for (int y = 0; y < size; y++)
    //         {
    //             if (!grid[x,y].alive)
    //             {
    //                 isGameOver= true;
    //             }
    //         }
    //     }
    //     if (!isGameOver)
    //     {
    //         for (int x = 0; x <size; x++)
    //         {
    //             for (int y = 0; y <size; y++)
    //             {
    //                 Instantiate(landPrefab, grid[x,y], Quaternion.identity);
    //             }
    //         }
    //     }
    // }
   
}
