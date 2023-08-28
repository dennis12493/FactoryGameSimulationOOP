using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InputProcessingBuilding : MonoBehaviour
{
    private int itemID;
    private int2 pos;
    private bool occupied;
    private int lastItemID;
    private Inventory inventory;
    
    void Start()
    {
        itemID = -1;
        lastItemID = -1;
        occupied = false;
        inventory = GetComponent<Inventory>();
        BuildingDictionary.Instance.InsertGameObjectInDictionary(new []{pos}, gameObject);
    }

    void Update()
    {
        occupied = inventory.IsInventoryFull(lastItemID);
        if (itemID.Equals(-1)) return;
        lastItemID = itemID;
        inventory.AddItem(new ItemAmount(itemID, 1));
        itemID = -1;
    }

    public void SetPosition(int2 pos)
    {
        this.pos = pos;
    }

    public int2 GetPosition()
    {
        return pos;
    }

    public void SetItem(int itemID)
    {
        this.itemID = itemID;
    }

    public int GetItem()
    {
        return itemID;
    }

    public bool IsOccupied()
    {
        return occupied;
    }
}
