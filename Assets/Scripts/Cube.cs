using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public enum CubeType
    {
        BORDER = 0,
        GROUND = 1,

        MOUNTAIN = 2,
        TREE = 3,

        GOLD_MINE = 4,
        STONE_MINE = 5,
        SAWMILL = 6,
        BUILDING = 7
    }

    public int X;
    public int Z;
    //public int ListX;
    //public int ListZ;

    public CubeType Type = CubeType.GROUND;
    public bool Occupied;

    public Color ColorOnHover;

    public Material BorderMaterial;
    public Material GroundMaterial;
    public Material MountainMaterial;
    public Material TreeMaterial;
    public Material GoldMineMaterial;
    public Material StoneMineMaterial;
    public Material SawmillMaterial;
    public Material BuildingMaterial;

    public GameObject GrassPrefab;
    public GameObject TreePrefab;
    public GameObject MountainPrefab;

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
                float rnd()
                {
                    return Random.Range(-0.5f, 0.5f);
                }
                for (int i = 0; i < 5; ++i)
                    Instantiate(GrassPrefab, new Vector3(X + rnd(), 0.5f, Z + rnd()), Quaternion.identity);
                break;

            case CubeType.MOUNTAIN:
                GetComponent<Renderer>().material = MountainMaterial;
                Instantiate(MountainPrefab, new Vector3(X, 0.5f, Z), Quaternion.identity);
                break;

            case CubeType.TREE:
                GetComponent<Renderer>().material = TreeMaterial;
                var t = Instantiate(TreePrefab, new Vector3(X, 0.5f, Z), Quaternion.identity);
                t.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
