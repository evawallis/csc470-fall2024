//FOR TITLE SCREEN
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject panel;

    public GameObject talkingFriends;
    // Start is called before the first frame update
    void Start()
    {
        talkingFriends.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                panel.SetActive(false);
                talkingFriends.SetActive(true);
            }
        }
    }
}
