using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    // Start is called before the first frame update

    public GameObject prefab;

    void Start()
    {
        GameObject[] spheres = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            // Vector3 position = transform.position + transform.forward * (i * 1.1f);
            // position.x += i;
            Vector3 position = transform.position;
            position.x = 3 * Mathf.Cos(i);
            position.y = 3 * Mathf.Sin(i);
            GameObject sphere = Instantiate(prefab, position, Quaternion.identity);
            spheres[i] = sphere;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
