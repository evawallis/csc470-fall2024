using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    CellScript[,] grid; //reference to a 2d array of cell scripts
    // can also do array of cell objects where cell has refernece to cell script

    public GameObject cellPrefab;

    float spacing = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        grid = new CellScript[10,10]; //instantiate grid with size 10x10
        for (int x = 0; x < 10; x++){
            for (int y = 0; y < 10; y++){
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
        
    }
}