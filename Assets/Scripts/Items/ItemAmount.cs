[System.Serializable]
public class ItemAmount
{
    public int itemID;
    public int amount;
    
    public ItemAmount(int itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;
    }
}
