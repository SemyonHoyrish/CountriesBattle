using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public struct BuildingCost
    {
        public int Money;
        public int Stones;
        public int Wood;

        public BuildingCost(int money, int stones, int wood)
        {
            Money = money;
            Stones = stones;
            Wood = wood;
        }
    }

    public enum BuildingType
    {
        None = -1,
        Mine = 1,
        Sawmill = 2,
    }


    public BuildingType Type = BuildingType.None;


    public static Dictionary<BuildingType, BuildingCost> BuildingCostByType = new Dictionary<BuildingType, BuildingCost>()
    {
        { BuildingType.Mine, new BuildingCost(100, 200, 50) },
        { BuildingType.Sawmill, new BuildingCost(100, 100, 110) },
    };



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BUILDIONG");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
