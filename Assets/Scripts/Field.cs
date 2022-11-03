using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static GameObject MainCamera;

    public GameObject CubePrefab;
    //public int Scale = 1;
    //public int Width;
    //public int Length;

    //public int ObjectToPlace = -1;
    public Building.BuildingType ObjectToBuild;
    //public GameObject[] ObjectsToPlace;
    
    public GameObject MinePrefab;
    public GameObject SawmillPrefab;


    //public Material[] CubesMaterials;

    public GameObject LastSelectedCube;

    private List<List<GameObject>> cubes = new List<List<GameObject>>();
    private GameObject lastPlacePreview;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = gameObject;

#if false
        int z_half = (Length / 2) * Scale;
        int x_half = (Width / 2) * Scale;
        for (int z = 0 - z_half; z < 0 + z_half; ++z)
        {
            cubes.Add(new List<GameObject>());
            for (int x = 0 - x_half; x < 0 + x_half; ++x)
            {
                var c = Instantiate(CubePrefab, new Vector3(x * Scale, 0, z * Scale), Quaternion.identity);

                if (Random.Range(1, 100) <= 10)
                {
                    c.GetComponent<Cube>().Type = Cube.CubeType.GOLD_MINE;
                    c.GetComponent<Renderer>().material.color = Color.yellow;
                }

                //cubes[z].Add(Instantiate(CubePrefab, new Vector3(x * Scale, 0, z * Scale), Quaternion.identity));
                //cubes[z][x].GetComponent<Cube>().ListX = x;
                //cubes[z][x].GetComponent<Cube>().ListZ = z;
            }
        }
#else

        //Debug.Log(System.IO.Directory.GetCurrentDirectory());
        MapParser.Map map = MapParser.Parse("1");

        for(int z = 0; z < map.Length; ++z)
        {
            for(int x = 0; x < map.Width; ++x)
            {
                var c = Instantiate(CubePrefab, new Vector3(x, 0, z), Quaternion.identity);
                c.GetComponent<Cube>().Type = (Cube.CubeType)map.Data[z][x];
            }
        }

#endif
    }

    private GameObject GetObjectToPlace()
    {
        switch (ObjectToBuild)
        {
            case Building.BuildingType.Mine:
                return MinePrefab;

            default:
                return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject objectToPlace = GetObjectToPlace();
        if (ObjectToBuild != Building.BuildingType.None && objectToPlace != null)
        {
            Destroy(lastPlacePreview);
            if (LastSelectedCube != null)
            {
                Vector3 pos = LastSelectedCube.transform.position;
                Cube cube = LastSelectedCube.GetComponent<Cube>();
                if (Input.GetMouseButtonDown(0))
                {
                    if (CanPlace(objectToPlace, cube))
                    {
                        Building.BuildingCost cost = Building.BuildingCostByType[ObjectToBuild];
                        World world = MainCamera.GetComponent<World>();

                        if (world.Money >= cost.Money &&
                            world.Stones >= cost.Stones &&
                            world.Wood >= cost.Wood)
                        {
                            GameObject building = Instantiate(objectToPlace, new Vector3(pos.x, pos.y + 0.5f, pos.z), Quaternion.identity);
                            cube.Occupied = true;
                            lastPlacePreview = null;
                            ObjectToBuild = Building.BuildingType.None;
                            building.AddComponent<Building>();

                            MainCamera.GetComponent<GameUI>().AddMessageToShow("Построено!", Color.green, 1);
                        }
                        else
                        {
                            MainCamera.GetComponent<GameUI>().AddMessageToShow(
                                $"Нехватает ресурсов: Золота: {Mathf.Abs(Mathf.Min(world.Money - cost.Money, 0))} Камня: {Mathf.Abs(Mathf.Min(world.Stones - cost.Stones, 0))} Дерева: {Mathf.Abs(Mathf.Min(world.Wood - cost.Wood, 0))}",
                                new Color(1.0f, 0.6f, 0f), 2);
                        }
                    }
                }
                else
                {
                    lastPlacePreview = Instantiate(objectToPlace, new Vector3(pos.x, pos.y + 1, pos.z), Quaternion.identity);
                    foreach(var m in lastPlacePreview.GetComponent<Renderer>().materials)
                    {
                        if (CanPlace(objectToPlace, cube))
                        {
                            m.color = Color.green + m.color;
                        }
                        else
                        {
                            m.color = Color.red + m.color;
                        }
                    }
                }
            }
        }
    }

    private bool CanPlace(GameObject obj, Cube cube)
    {
        if (cube.Occupied) return false;

        switch(obj.name)
        {
            case "mine":
                if (cube.Type == Cube.CubeType.GOLD_MINE ||
                    cube.Type == Cube.CubeType.STONE_MINE)
                    return true;
                break;

            default:
                Debug.LogError("Undefined object to place! " + obj.name);
                break;
        }
        return false;
    }
}
