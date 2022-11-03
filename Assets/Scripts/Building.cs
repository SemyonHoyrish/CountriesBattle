using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public struct BuildingCost
    {
        public int Gold;
        public int Stones;
        public int Wood;

        public BuildingCost(int gold, int stones, int wood)
        {
            Gold = gold;
            Stones = stones;
            Wood = wood;
        }
    }

    public struct ProductionAmount
    {
        public int Gold;
        public int Stones;
        public int Wood;

        public ProductionAmount(int gold, int stones, int wood)
        {
            Gold = gold;
            Stones = stones;
            Wood = wood;
        }
    }

    public enum BuildingType
    {
        None = -1,
        GoldMine = 1,
        StoneMine = 2,
        Sawmill = 3,
        ArmyBase = 4,
    }


    public BuildingType Type = BuildingType.None;


    public static Dictionary<BuildingType, BuildingCost> BuildingCostByType = new Dictionary<BuildingType, BuildingCost>()
    {
        { BuildingType.GoldMine, new BuildingCost(100, 200, 50) },
        { BuildingType.StoneMine, new BuildingCost(100, 200, 50) },
        { BuildingType.Sawmill, new BuildingCost(100, 100, 110) },
        { BuildingType.ArmyBase, new BuildingCost(600, 360, 220) },
    };


    public ProductionAmount GetProduction()
    {
        switch (Type)
        {
            case BuildingType.GoldMine:
                return new ProductionAmount(2, 0, 0);
            case BuildingType.StoneMine:
                return new ProductionAmount(0, 4, 0);
            case BuildingType.Sawmill:
                return new ProductionAmount(0, 0, 6);
            default:
                return new ProductionAmount(0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
