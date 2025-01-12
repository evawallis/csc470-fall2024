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

    public GameObject winText;

    public static GameManager instance;

    public GameObject snowmanPrefab;

    LayerMask layerMask;
    public Vector3 destination;

    public List<UnitScript> units = new List<UnitScript>();

    public UnitScript selectedUnit;

    Transform unitSelectedPoint;
    Transform snowBallSelectedPoint;

    Vector3 savedPosition;
    

    public GameObject popUpWindow;

    public TMP_Text nameText;
    
    public TMP_Text bioText;

    public TMP_Text statText;

    public Image portraitImage;

    public Camera mainCamera;

    int numSnowballs = 0;
    public TMP_Text snowBallNumText;


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

    void Update()
{
    snowBallNumText.text = numSnowballs.ToString();
    // Handle space bar press
    if (Input.GetKeyDown(KeyCode.Space))
    {
        SpaceBarPressed?.Invoke();
    }

    // Handle mouse input
    if (Input.GetMouseButtonDown(0))
    {
        Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.collider.CompareTag("ground"))
            {
                Debug.Log("Ground hit!");
                if (selectedUnit != null)
                {
                    selectedUnit.nma.SetDestination(hitInfo.point); // Set destination for the unit
                }
            }
            else if (hitInfo.collider.CompareTag("unit"))
            {
                // select a new unit
                UnitClicked?.Invoke(hitInfo.collider.gameObject.GetComponent<UnitScript>());
                selectedUnit = hitInfo.collider.gameObject.GetComponent<UnitScript>();
                unitSelectedPoint = hitInfo.collider.gameObject.transform;
            }
            else if (hitInfo.collider.CompareTag("snowball"))
            {
                snowBallSelectedPoint = hitInfo.collider.gameObject.transform;
                if (selectedUnit != null)
                {
                    // Set destination to snowball
                    selectedUnit.nma.SetDestination(hitInfo.point);
                    Debug.Log("Snowball selected!");
                }
            }
        }
    }

    if (selectedUnit != null && snowBallSelectedPoint != null)
    {
        if (!selectedUnit.nma.pathPending && selectedUnit.nma.remainingDistance <= selectedUnit.nma.stoppingDistance)
        {
            if (Vector3.Distance(selectedUnit.transform.position, snowBallSelectedPoint.position) < 5f)
            {
                Debug.Log("Snowball collected!");
                savedPosition = snowBallSelectedPoint.position;
                Destroy(snowBallSelectedPoint.gameObject);
                snowBallSelectedPoint = null; // Reset the snowball selection
                numSnowballs++;

                if (numSnowballs >= 10)
                {
                    Instantiate(snowmanPrefab, savedPosition, Quaternion.identity);
                    Debug.Log("Snowman instantiated!");
                    winText.SetActive(true);
                }
            }
        }
    }
}

    public void SelectUnit(UnitScript unit)
    {
        UnitClicked?.Invoke(unit);
    
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
