using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    
   
    public string name;
    public NavMeshAgent nma;
    public string bio;
    public string stats;
    // public GameObject gameManagerObject;
    public bool selected = false;
    public Color selectedColor;
    public Color normalColor;
    public Renderer bodyRenderer;
    int numSnowballs = 0;

    // public Vector3 destination;
    public GameObject wallSeeingSphere;
    float rotateSpeed;

    LayerMask layerMask;

    public Vector3 destination;

    void OnEnable()
    {
        GameManager.instance.SpaceBarPressed += ChangeToRandomColor;
        GameManager.instance.UnitClicked += GameManagerSaysUnitWasClicked;

    }

    void OnDisable()
    {
        GameManager.instance.SpaceBarPressed -= ChangeToRandomColor;
        GameManager.instance.UnitClicked -= GameManagerSaysUnitWasClicked;

    }

    void GameManagerSaysUnitWasClicked(UnitScript unit)
    {
        if (unit == this)
        {
            selected = true;
            bodyRenderer.material.color = selectedColor;
        }
        else
        {
            selected = false;
            bodyRenderer.material.color = normalColor;
        }
    }

    void ChangeToRandomColor()
    {
        bodyRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("wall");

        GameManager.instance.units.Add(this);

        rotateSpeed = Random.Range(20, 60);

        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
    }

    

    // Update is called once per frame
    void Update()
    {
        // if(destination != null)
        // {
        //     Vector3 direction = destination - transform.position;
        //     direction.Normalize();
        //     transform.position += direction * 5 * Time.deltaTime;
        // }
        // Vector3 rayStart = transform.position + Vector3.up * 1.75f;
        // RaycastHit hit;
        // if (Physics.Raycast(rayStart, transform.forward, out hit, Mathf.Infinity, layerMask))
        // {
        //     wallSeeingSphere.SetActive(true);
        //     wallSeeingSphere.transform.position = hit.point;
        // }
        // else 
        // {
        //     wallSeeingSphere.SetActive(false);
        // }
    }

    void OnMouseDown()
    {
        // Debug.Log(this.name);
        // selected = true;
        // bodyRenderer.material.color = selectedColor;
        // GameObject gameManagerObject = GameObject.Find("GameManagerObject");
        // GameObject gm = gameManagerObject.GetComponent<GameManager>();
        // GameManager.instance.popUpWindow.SetActive(true);
        // GameManager.instance.nameText.text = name;
        // GameManager.instance.bioText.text = bio;
        // GameManager.instance.statText.text = stats;
        // GameManager.instance.OpenCharacterSheet(this);
        // GameManager.instance.SelectUnit(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snowball"))
        {
            Destroy(other);
            numSnowballs++;
        }
    }
}
