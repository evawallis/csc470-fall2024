using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EndSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            // If no EventSystem exists, create one
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystem = eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>(); // Default input module for the EventSystem
            Debug.Log("Event System created.");
        }
        else
        {
            Debug.Log("Event System already exists.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        Debug.Log("started end scene");
    }

    public void EndScene()
    {
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
    }
}
