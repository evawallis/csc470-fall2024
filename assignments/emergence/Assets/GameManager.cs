using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    SoilScript[,] grid;
    public GameObject soilPrefab;
    float spacing = 1.1f;
    int size = 10;
    // Start is called before the first frame update
    void Start()
    {
        grid = new SoilScript[size,size]; //instantiate grid with size 10x10
        for (int x = 0; x < size; x++){
            for (int y = 0; y < size; y++){
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing; //so grid can be on the ground 
                GameObject soil = Instantiate(soilPrefab, pos, Quaternion.identity);//Quaternion.identity is no rotation
                grid[x,y] = soil.GetComponent<SoilScript>();
                grid[x,y].alive = (Random.value > 0.5f); //random.value returns a value between 0 and 1, so if its greater itll be true (flipping a coin)
                if (grid[x,y].alive == true)
                {
                    grid[x,y].MakeSeedling();
                }
                grid[x,y].xIndex = x;
                grid[x,y].yIndex = y;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            GenerateWeeds(Random.Range(0, 10), Random.Range(0, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void GenerateWeeds(int x, int y)
    {
        grid[x, y].MakeWeed();
    }
    
    public void KillNeighbors(int xIndex, int yIndex)
    {
        for (int x = xIndex - 1; x <= xIndex + 1; x++)
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {
               try
                {
                    if (!(x== xIndex && y== yIndex))
                    {
                        if (grid[x,y].alive)//if its not ourself and its alive
                        {
                            grid[x,y].Kill();
                            Debug.Log("killed" + x + " " + y);
                        }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    continue;
                } 
            }
        }
    }
}
