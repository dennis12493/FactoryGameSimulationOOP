using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Position : MonoBehaviour
{
    private int2[] positions;

    public void SetPositions(int2[] positions)
    {
        this.positions = positions;
    }
}
