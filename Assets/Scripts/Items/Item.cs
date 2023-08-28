using Unity.Mathematics;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int2 pos;
    [SerializeField] private int itemID;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, -0.5f), Time.deltaTime * 2f);
    }

    public void SetPosition(int2 pos)
    {
        this.pos = pos;
    }

    public int GetItemID()
    {
        return itemID;
    }
}