using UnityEngine;

public class Items : MonoBehaviour
{
    public static Items INSTANCE { get; private set; }
    public GameObject ironIngot;
    public GameObject ironOre;
    public GameObject coal;
    public GameObject steelIngot;

    private void Start()
    {
        INSTANCE = this;
    }

    public GameObject GetItem(int itemID)
    {
        switch (itemID)
        {
            case ItemAssets.COAL: return coal;
            case ItemAssets.IRON_ORE: return ironOre;
            case ItemAssets.IRON_INGOT: return ironIngot;
            case ItemAssets.STEEL: return steelIngot;
        }
        return null;
    }

}
