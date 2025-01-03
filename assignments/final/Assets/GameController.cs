using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Image irisMask;

    float fadeDuration = 1.5f;
    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = irisMask.rectTransform.localScale;
        if (SceneManager.GetActiveScene().name == "GameplayScene")
        {
            StartCoroutine(IrisOutCoroutine());
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
