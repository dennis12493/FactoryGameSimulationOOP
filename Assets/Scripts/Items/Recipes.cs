using System;
using UnityEngine;

public class Recipes : MonoBehaviour
{
    public static Recipes Instance { get; private set; }

    [SerializeField] private Recipe steelIngotRecipe;
    [SerializeField] private Recipe ironIngotRecipe;
    [SerializeField] private Recipe coalMineRecipe;
    [SerializeField] private Recipe ironMineRecipe;

    private void Awake()
    {
        Instance = this;
    }
    
    public Recipe GetSteelIngotRecipe()
    {
        return steelIngotRecipe;
    }

    public Recipe GetIronIngotRecipe()
    {
        return ironIngotRecipe;
    }

    public Recipe GetCoalMineRecipe()
    {
        return coalMineRecipe;
    }

    public Recipe GetIronMineRecipe()
    {
        return ironMineRecipe;
    }
    
    public Recipe GetMineRecipe(String tilename)
    {
        var id = ItemAssets.Instance.GetItemID(tilename);
        switch (id)
        {
            case ItemAssets.COAL: return coalMineRecipe;
            case ItemAssets.IRON_ORE: return ironMineRecipe;
            default: return null;
        }
    }

}
