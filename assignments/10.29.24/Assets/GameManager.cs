using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public List<UnitScript> units = new List<UnitScript>();

    public UnitScript selectedUnit;

    public GameObject popUpWindow;

    public TMP_Text nameText;
    
    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    // Start is called before the first frame update
    
    void OnEnable() //happens before start
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
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

    public void SelectUnit(UnitScript unit)
    {
        //deselect all other units
        foreach(UnitScript u in units)
        {
            u.selected = false;
            u.bodyRenderer.material.color = u.normalColor;

        }
        //select new unit
        unit.selected = true;
        unit.bodyRenderer.material.color = unit.selectedColor;
        Debug.Log(unit.name);
        selectedUnit = unit;
    }

    public void OpenCharacterSheet(UnitScript unit)
    {
        nameText.text = unit.name;
        bioText.text = unit.bio;
        statText.text = unit.stats;
        popUpWindow.SetActive(true);
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
