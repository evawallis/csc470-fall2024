using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject popUpWindow;

    public TMP_Text nameText;
    
    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "Snoopy";
        bioText.text = "hello i snoopy";
        statText.text = "wins: 1 million";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
