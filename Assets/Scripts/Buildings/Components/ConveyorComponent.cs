using Unity.Mathematics;
using UnityEngine;

public class ConveyorComponent
{
    public GameObject item;
    public int2 pos;

    public ConveyorComponent(int2 pos)
    {
        this.pos = pos;

    }
}
