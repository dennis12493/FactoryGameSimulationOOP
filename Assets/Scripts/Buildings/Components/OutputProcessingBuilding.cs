using System.Collections;
using System.Collections.Generic;
using Buildings.Components;
using Unity.Mathematics;
using UnityEngine;

public class OutputProcessingBuilding : MonoBehaviour
{
    private GameObject outputGameObject;
    private int2 pos;
    private int itemID;
    private bool itemCreated;
    
    void Start()
    {
        outputGameObject = null;
        itemID = -1;
        itemCreated = true;
    }

    void Update()
    {
        if(itemID == -1) return;
        outputGameObject ??= BuildingDictionary.Instance.GetGameObjectAtPosition(pos);
        if (ReferenceEquals(outputGameObject, null) ||
            !outputGameObject.TryGetComponent(out InputConveyorComponent input)) return;
        if(input.IsOccupied() || !ReferenceEquals(input.GetItem(), null)) return;
        var itemGameObject = Items.INSTANCE.GetItem(itemID);
        var item = Instantiate(itemGameObject, new Vector3(pos.x, pos.y, -0.5f), Quaternion.identity);
        item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        var itemComponent = item.GetComponent<Item>();
        itemComponent.SetPosition(pos);
        var inputConveyorComponent = outputGameObject.GetComponent<InputConveyorComponent>();
        inputConveyorComponent.SetItem(item);
        itemID = -1;
        itemCreated = true;
    }

    public void SetPosition(int2 pos)
    {
        this.pos = pos;
    }

    public void SetItem(int itemID)
    {
        this.itemID = itemID;
    }

    public void SetItemCreated(bool itemCreated)
    {
        this.itemCreated = itemCreated;
    }

    public bool GetItemCreated()
    {
        return itemCreated;
    }
}
