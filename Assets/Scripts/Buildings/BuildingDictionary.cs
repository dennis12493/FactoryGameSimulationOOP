using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class BuildingDictionary : MonoBehaviour
{

    public static BuildingDictionary Instance { get; private set;}
    private Dictionary<int2, GameObject> buildings;

    void Awake()
    {
        Instance = this;
        buildings = new Dictionary<int2, GameObject>();
    }

    public GameObject GetGameObjectAtPosition(int2 key)
    {
        if (buildings.ContainsKey(key)) return buildings[key];
        return null;
    }
    
    public bool CheckIfOccupied(params int2[] int2s){
        foreach(int2 v in int2s){
            if(buildings.ContainsKey(v)) return true;
        }
        return false;
    }

    public bool IsConveyor(int2 pos)
    {
        GameObject building;
        if (buildings.TryGetValue(pos, out building))
        {
            return building.CompareTag("Conveyorbelt");
        }
        return false;
    }

    public bool IsBuilding(int2 pos)
    {
        GameObject building;
        if (buildings.TryGetValue(pos, out building))
        {
            return building.CompareTag("Smelter") || building.CompareTag("Assembler") || building.CompareTag("Trashcan");;
        }
        return false;
    }

    public void InsertGameObjectInDictionary(int2[] position, GameObject gameObject)
    {
        foreach (int2 pos in position) {
            buildings[pos] = gameObject;
        }
    }

    public void DeleteGameObjectAtPosition(int2 position)
    {
        if (buildings.ContainsKey(position))
        {
            buildings.Remove(position);
            Destroy(buildings[position]);
        }
    }
}