using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Recipe")]
public class Recipe : ScriptableObject
{
    public List<ItemAmount> requiredItems;
    public float timeToCraft;
    public int outputItemID;
}
