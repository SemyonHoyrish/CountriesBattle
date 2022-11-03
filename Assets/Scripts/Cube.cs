using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public enum CubeType
    {
        BORDER = 0,
        GROUND = 1,

        CAVE = 2,
        TREE = 3,

        GOLD_MINE = 4,
        STONE_MINE = 5,
        SAWMILL = 6,
        BUILDING = 7
    }
        

    //public int ListX;
    //public int ListZ;

    public CubeType Type = CubeType.GROUND;
    public bool Occupied;

    public Color ColorOnHover;

    public Material BorderMaterial;
    public Material GroundMaterial;
    public Material CaveMaterial;
    public Material TreeMaterial;
    public Material GoldMineMaterial;
    public Material StoneMineMaterial;
    public Material SawmillMaterial;
    public Material BuildingMaterial;

    private Color colorBeforeHover;

    // Start is called before the first frame update
    void Start()
    {
        switch(Type)
        {
            case CubeType.BORDER:
                GetComponent<Renderer>().material = BorderMaterial;
                break;

            case CubeType.GROUND:
                GetComponent<Renderer>().material = GroundMaterial;
                break;

            case CubeType.CAVE:
                GetComponent<Renderer>().material = CaveMaterial;
                break;

            case CubeType.TREE:
                GetComponent<Renderer>().material = TreeMaterial;
                break;

            case CubeType.GOLD_MINE:
                GetComponent<Renderer>().material = GoldMineMaterial;
                break;

            case CubeType.STONE_MINE:
                GetComponent<Renderer>().material = StoneMineMaterial;
                break;

            case CubeType.SAWMILL:
                GetComponent<Renderer>().material = SawmillMaterial;
                break;

            case CubeType.BUILDING:
                GetComponent<Renderer>().material = BuildingMaterial;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        colorBeforeHover = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = colorBeforeHover + ColorOnHover;
        Field.MainCamera.gameObject.GetComponent<Field>().LastSelectedCube = gameObject;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = colorBeforeHover;
        Field.MainCamera.gameObject.GetComponent<Field>().LastSelectedCube = null;
    }
}
