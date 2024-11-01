using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public string name;
    public string bio;
    public string stats;
    // public GameObject gameManagerObject;
    public bool selected = false;
    public Color selectedColor;
    public Color normalColor;
    public Renderer bodyRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.units.Add(this);
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
    }

    

    // Update is called once per frame
    void Update()
    {
        
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
        GameManager.instance.SelectUnit(this);
    }
}
