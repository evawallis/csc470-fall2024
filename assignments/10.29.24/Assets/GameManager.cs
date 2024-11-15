using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{

    public Action SpaceBarPressed;
    public Action<UnitScript> UnitClicked;

    public static GameManager instance;

    LayerMask layerMask;
    public Vector3 destination;

    public List<UnitScript> units = new List<UnitScript>();

    public UnitScript selectedUnit;

    

    public GameObject popUpWindow;

    public TMP_Text nameText;
    
    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    public Camera mainCamera;


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
        layerMask = LayerMask.GetMask("ground", "unit");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceBarPressed?.Invoke();
        }




        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mousePositionRay,out hitInfo, Mathf.Infinity, layerMask))
            {
                if (hitInfo.collider.CompareTag("ground"))
                {
                    Debug.Log("ground hit!");
                    if (selectedUnit != null)
                    {
                        // selectedUnit.gameObject.transform.position =hitInfo.point;
                        // selectedUnit.destination = hitInfo.point;
                        selectedUnit.nma.SetDestination(hitInfo.point);
                    }
                } 
                else if (hitInfo.collider.CompareTag("unit"))
                {
                    // SelectUnit(hitInfo.collider.gameObject.GetComponent<UnitScript>());
                    UnitClicked?.Invoke(hitInfo.collider.gameObject.GetComponent<UnitScript>());
                    selectedUnit = hitInfo.collider.gameObject.GetComponent<UnitScript>();
                }
                
            }
        }
    }

    public void SelectUnit(UnitScript unit)
    {
        UnitClicked?.Invoke(unit);
        //deselect all other units
        // foreach(UnitScript u in units)
        // {
        //     u.selected = false;
        //     u.bodyRenderer.material.color = u.normalColor;

        // }
        //select new unit
        // unit.selected = true;
        // unit.bodyRenderer.material.color = unit.selectedColor;
        // Debug.Log(unit.name);
        // selectedUnit = unit;
    }

    public void OpenCharacterSheet()
    {
        nameText.text = selectedUnit.name;
        bioText.text = selectedUnit.bio;
        statText.text = selectedUnit.stats;
        popUpWindow.SetActive(true);
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
