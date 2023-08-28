using UnityEngine;

public class ProcessingBuilding : MonoBehaviour
{
    
    private Recipe recipe;
    private float timer;
    private bool isProducing;
    private OutputProcessingBuilding output;
    private Inventory inventory;
    
    void Start()
    {
        timer = 500f;
        isProducing = false;
        output = GetComponent<OutputProcessingBuilding>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (isProducing)
        {
            timer -= Time.deltaTime;
            if (timer > 0) return;
            output.SetItem(recipe.outputItemID);
            output.SetItemCreated(false);
            isProducing = false;
            timer = recipe.timeToCraft;
        }
        else if(!isProducing && output.GetItemCreated() && inventory.CanCraft(recipe.requiredItems))
        {
            inventory.RemoveItems(recipe.requiredItems);
            timer = recipe.timeToCraft;
            isProducing = true;
        }
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
    }
}
