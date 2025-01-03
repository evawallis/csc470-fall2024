//FOR TITLE SCREEN
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject panel;

    public GameObject talkingFriends;

    public TMP_Text message; 

    public Animator puduAnim;

    public Animator sparrowAnim;

    public GameObject textBox;

    public Image irisMask;

    public GameObject imageCover;
    float fadeDuration = 1.5f;
    Vector3 initialScale;

    string[] dialogueLines;

    int dialogueIndex;

    // bool isShowingDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = irisMask.rectTransform.localScale;
        talkingFriends.SetActive(false);
        dialogueLines = new string[]
        {
            "Well, well, what do we have here? Another early riser! Good morning, little sparrow. You're just in time!",

            "Good morning! Who are you? And... in time for what?",

            "I'm your guide, Pudu. And you, my feathered friend, are about to embark on the adventure of a lifetime!",

            "Adventure? What kind of adventure? I was just looking for breakfast...", 

            "Breakfast, eh? Let’s just say the early bird might get the worm... but only if they’re clever enough to find it! You’ll need to explore this world, collect clues, and outsmart... well, I won’t spoil the surprises!",

            "Start by stretching those wings and keeping your eyes sharp. Seeds scattered across this world hold secrets... and each one will bring you closer to your prize. But be careful—things aren’t always as simple as they seem!",

            "Got it. Find the seeds, uncover the clues, and get the worm. I won’t let you down, Pudu!",

            "I know you won’t, little sparrow. The world is waiting—go on, spread those wings and show us what you’ve got!"
        };
        dialogueIndex = 0;
        // isShowingDialogue = false;
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
                startConvo();
            }
            // else if (talkingFriends.activeSelf && Input.GetKeyDown(KeyCode.Space))
            // {
            //     startConvo();
            // }
            
        }
        else if (talkingFriends.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            startConvo();
        }
    }

    void startConvo()
    {
        if (dialogueIndex < dialogueLines.Length)
        {
            int[] puduLines = {0, 2, 4, 5, 7};
            message.text = dialogueLines[dialogueIndex];

            if (puduLines.Contains(dialogueIndex))
            {
                puduTalk();
            }
            else
            {
                sparrowTalk();
            }

            dialogueIndex++;
        }
        else
        {
            endConvo();
        }

    }



    void puduTalk()
    {
        puduAnim.SetBool("isTalking", true);
        sparrowAnim.SetBool("isTalking", false);
    }

    void sparrowTalk()
    {
        puduAnim.SetBool("isTalking", false);
        sparrowAnim.SetBool("isTalking", true);
    }

    void nextLine()
    {
        message.text = "Good morning! Who are you? And... in time for what?";
        sparrowTalk();
    }

    void endConvo()
    {
        Destroy(textBox);
        puduAnim.SetBool("isTalking", false);
        sparrowAnim.SetBool("isTalking", false);
        puduAnim.SetBool("isSpinning", true);
        sparrowAnim.SetBool("isSpinning", true);
        StartCoroutine(IrisOutCoroutine());    
        
    }

    IEnumerator IrisOutCoroutine()
    {
        float elapsedTime = 0f;

        Vector3 endScale = new Vector3(-0.39f, -0.39f, -0.39f);

        while (elapsedTime < fadeDuration)
        {
            
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            // Scale down the mask
            irisMask.rectTransform.localScale = Vector3.Lerp(initialScale, endScale, progress);

            yield return null;
        }

        irisMask.rectTransform.localScale = endScale; // Ensure it's fully faded
        imageCover.SetActive(true);
        Debug.Log("Iris out completed!");
        SceneManager.LoadScene("GameplayScene");
 

    }

    
}
