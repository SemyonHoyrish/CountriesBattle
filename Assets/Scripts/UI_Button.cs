using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{

    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var btn in buttons)
        {
            btn.GetComponent<Button>().onClick.AddListener(() => { ButtonClicked(btn.name); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(string name)
    {
        switch(name)
        {
            case "BuildMineButton":
                //GetComponent<Field>().ObjectToPlace = 0;
                GetComponent<Field>().ObjectToBuild = Building.BuildingType.Mine;
                break;

            default:
                Debug.LogError("Undefined button " + name);
                break;

        }
    }
}
