using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameController : MonoBehaviour
{

    public Image irisMask;
    public GameObject canvas;

    


    float fadeDuration = 1.5f;
    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = irisMask.rectTransform.localScale;
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "GameplayScene")
        {
            canvas.SetActive(true);
            StartCoroutine(IrisOutCoroutine());
            Destroy(irisMask);
        }
        
    }

    void Awake()
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

    IEnumerator IrisOutCoroutine()
    {
        float elapsedTime = 0f;

        Vector3 endScale = new Vector3(-10, -10, -10);

        while (elapsedTime < fadeDuration)
        {
            
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            // Scale down the mask
            irisMask.rectTransform.localScale = Vector3.Lerp(initialScale, endScale, progress);

            yield return null;
        }

        irisMask.rectTransform.localScale = endScale; // Ensure it's fully faded
    }
}
