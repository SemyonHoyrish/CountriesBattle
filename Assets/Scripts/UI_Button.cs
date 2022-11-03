using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{

    public Button[] buttons;
    public Transform Canvas;

    // Start is called before the first frame update
    void Start()
    {
        List<Button> btns = new List<Button>();
        foreach(Transform c in Canvas.transform)
        {
            if (c.GetComponent<Button>() != null)
                btns.Add(c.GetComponent<Button>());
        }

        buttons = btns.ToArray();

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
            case "BuildGoldMineButton":
                GetComponent<Field>().ObjectToBuild = Building.BuildingType.GoldMine;
                break;
            case "BuildStoneMineButton":
                GetComponent<Field>().ObjectToBuild = Building.BuildingType.StoneMine;
                break;
            case "BuildSawmillButton":
                GetComponent<Field>().ObjectToBuild = Building.BuildingType.Sawmill;
                break;
            case "BuildArmyBaseButton":
                GetComponent<Field>().ObjectToBuild = Building.BuildingType.ArmyBase;
                break;

            default:
                Debug.LogError("Undefined button " + name);
                break;

        }
    }
}
