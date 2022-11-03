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
    public GameObject ArmyBasePrefab;


    //public Material[] CubesMaterials;

    public GameObject LastSelectedCube;

    private List<List<GameObject>> cubes = new List<List<GameObject>>();
    private GameObject lastPlacePreview;

    private float time = 0f;

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
                Cube cube = c.GetComponent<Cube>();
                cube.Type = (Cube.CubeType)map.Data[z][x];
                cube.X = x;
                cube.Z = z;
            }
        }

#endif
    }

    private GameObject GetObjectToPlace()
    {
        switch (ObjectToBuild)
        {
            case Building.BuildingType.GoldMine:
            case Building.BuildingType.StoneMine:
                return MinePrefab;

            case Building.BuildingType.Sawmill:
                return SawmillPrefab;

            case Building.BuildingType.ArmyBase:
                return ArmyBasePrefab;

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
                    if (CanPlace(cube))
                    {
                        Building.BuildingCost cost = Building.BuildingCostByType[ObjectToBuild];
                        World world = MainCamera.GetComponent<World>();

                        if (world.Gold >= cost.Gold &&
                            world.Stones >= cost.Stones &&
                            world.Wood >= cost.Wood)
                        {
                            GameObject building = Instantiate(objectToPlace, new Vector3(pos.x, pos.y + 0.5f, pos.z), Quaternion.identity);
                            building.AddComponent<Building>();
                            building.GetComponent<Building>().Type = ObjectToBuild;
                            cube.Occupied = true;

                            lastPlacePreview = null;
                            ObjectToBuild = Building.BuildingType.None;
                            

                            world.Gold -= cost.Gold;
                            world.Stones -= cost.Stones;
                            world.Wood -= cost.Wood;

                            MainCamera.GetComponent<GameUI>().AddMessageToShow("Построено!", Color.green, 1);
                        }
                        else
                        {
                            MainCamera.GetComponent<GameUI>().AddMessageToShow(
                                $"Нехватает ресурсов: Золота: {Mathf.Abs(Mathf.Min(world.Gold - cost.Gold, 0))} Камня: {Mathf.Abs(Mathf.Min(world.Stones - cost.Stones, 0))} Дерева: {Mathf.Abs(Mathf.Min(world.Wood - cost.Wood, 0))}",
                                new Color(1.0f, 0.6f, 0f), 2);
                        }
                    }
                }
                else
                {
                    lastPlacePreview = Instantiate(objectToPlace, new Vector3(pos.x, pos.y + 1, pos.z), Quaternion.identity);
                    foreach(var m in lastPlacePreview.GetComponent<Renderer>().materials)
                    {
                        if (CanPlace(cube))
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(lastPlacePreview);
            lastPlacePreview = null;
            ObjectToBuild = Building.BuildingType.None;
        }
    }

    private void FixedUpdate()
    {
        
        time += Time.fixedDeltaTime;
        
        if (time >= 1f)
        {
            Building[] buildings = FindObjectsOfType<Building>();
            World w = GetComponent<World>();

            foreach(var b in buildings)
            {
                Building.ProductionAmount pr = b.GetProduction();
                w.Gold += pr.Gold;
                w.Stones += pr.Stones;
                w.Wood += pr.Wood;
            }

            time = 0f;
        }

    }

    private bool CanPlace(Cube cube)
    {
        if (cube.Occupied) return false;

        switch (ObjectToBuild)
        {
            case Building.BuildingType.GoldMine:
                if (cube.Type == Cube.CubeType.GOLD_MINE) return true;
                break;

            case Building.BuildingType.StoneMine:
                if (cube.Type == Cube.CubeType.STONE_MINE) return true;
                break;

            case Building.BuildingType.Sawmill:
                if (cube.Type == Cube.CubeType.SAWMILL) return true;
                break;

            case Building.BuildingType.ArmyBase:
                if (cube.Type == Cube.CubeType.BUILDING) return true;
                break;
        }

        //switch(obj.name)
        //{
        //    case "mine":
        //        if (cube.Type == Cube.CubeType.GOLD_MINE && ObjectToBuild == Building.BuildingType.GoldMine ||
        //            cube.Type == Cube.CubeType.STONE_MINE && ObjectToBuild == Building.BuildingType.StoneMine)
        //            return true;
        //        break;

        //    case "sawmill":
        //        if (cube.Type == Cube.CubeType.SAWMILL) return true;
        //        break;

        //    default:
        //        Debug.LogError("Undefined object to place! " + obj.name);
        //        break;
        //}
        return false;
    }
}
