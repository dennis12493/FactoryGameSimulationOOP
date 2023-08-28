using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemAmount> items;
    
    void Start()
    {
        items = new List<ItemAmount>();
    }

    public void AddItem(ItemAmount item)
    {
        foreach (var i in items)
        {
            if (i.itemID.Equals(item.itemID))
            {
                i.amount += item.amount;
                return;
            }
        }
        items.Add(item);
    }

    public void RemoveItems(List<ItemAmount> recipeItems)
    {
        foreach (var recipeItem in recipeItems)   
        {
            foreach (var item in items)
            {
                if (recipeItem.itemID.Equals(item.itemID))
                {
                    item.amount -= recipeItem.amount;
                }
            }
        }
    }

    public bool IsInventoryFull(int itemID)
    {
        foreach (var item in items)
        {
            if (item.itemID.Equals(itemID)) return item.amount >= 10;
        }
        return false;
    }

    public bool CanCraft(List<ItemAmount> requiredItems)
    {
        foreach (var inputItem in requiredItems)
        {
            bool containing = false;
            foreach (var item in items)
            {
                if (inputItem.itemID.Equals(item.itemID))
                {
                    if (item.amount >= inputItem.amount)
                    {
                        containing = true;
                    }
                }
            }
            if (containing == false) return false;
        }
        return true;
    }

    public void Clear()
    {
        items.Clear();
    }
}
