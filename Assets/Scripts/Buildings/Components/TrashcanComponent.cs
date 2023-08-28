using UnityEngine;

public class TrashcanComponent : MonoBehaviour
{
    public Inventory inventory;
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        inventory.Clear();
    }
}
