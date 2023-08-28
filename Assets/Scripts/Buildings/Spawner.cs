using Buildings.Components;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject miningDrill;
    [SerializeField] private GameObject smelter;
    [SerializeField] private GameObject conveyorUI;
    [SerializeField] private GameObject assembler;
    [SerializeField] private GameObject trashcan;
    public static Spawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnConveyor(Vector2 start, Vector2 end, float rotation)
    {
        var distance = (int) Vector2.Distance(start, end);
        Vector2 direction = (end - start).normalized;

        if (direction.Equals(Vector2.zero))
        {
            switch (rotation)
            {
                case 0:
                    direction = new Vector2(1, 0);
                    break;
                case 90:
                    direction = new Vector2(0, 1);
                    break;
                case 180:
                    direction = new Vector2(-1, 0);
                    break;
                case 270:
                    direction = new Vector2(0, -1);
                    break;
            }
        }

        var beltPathGameObject = new GameObject("BeltPath");
        var beltPath = beltPathGameObject.AddComponent<BeltPath>();
        for (int i = 0; i < distance + 1; i++)
        {
            Vector2 pos = start + (direction * i);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(conveyorUI, new Vector3(pos.x, pos.y, -0.1f), Quaternion.AngleAxis(angle, Vector3.forward));
            beltPath.AddConveyor(new ConveyorComponent(new int2((int)pos.x, (int)pos.y)));
        }

        var input = beltPathGameObject.AddComponent<InputConveyorComponent>();
        input.SetPosition(new int2(start));
        var output = beltPathGameObject.AddComponent<OutputConveyorComponent>();
        output.SetPosition(new int2(end + direction));
        var transform = beltPathGameObject.GetComponent<Transform>();
        transform.position = start;
        return beltPathGameObject;
    }

    public GameObject SpawnMiningDrill(int2 pos, Quaternion rotation, Recipe recipe)
    {
        int2 output = int2.zero;
        switch (rotation.eulerAngles.z)
        {
            case 0:
                output = new int2(pos.x + 1, pos.y);
                break;
            case 90:
                output = new int2(pos.x, pos.y + 1);
                break;
            case 180:
                output = new int2(pos.x - 1, pos.y);
                break;
            case 270:
                output = new int2(pos.x, pos.y - 1);
                break;
        }
        var miningDrillGameObject = Instantiate(miningDrill, new Vector3(pos.x, pos.y, 0f), rotation);
        var outputProcessingBuilding = miningDrillGameObject.GetComponent<OutputProcessingBuilding>();
        outputProcessingBuilding.SetPosition(output);
        var processingBuilding = miningDrillGameObject.GetComponent<ProcessingBuilding>();
        processingBuilding.SetRecipe(recipe);
        SetPositions(miningDrill, new []{pos});
        return miningDrill;
    }

    public GameObject SpawnSmelter(int2 pos, Quaternion rotation, Recipe recipe)
    {
        int2 output = int2.zero, pos2 = int2.zero;
        switch (rotation.eulerAngles.z)
        {
            case 0:
                output = new int2(pos.x + 2, pos.y);
                pos2 = new int2(pos.x + 1, pos.y);
                break;
            case 90:
                output = new int2(pos.x, pos.y + 2);
                pos2 = new int2(pos.x, pos.y + 1);
                break;
            case 180:
                output = new int2(pos.x - 2, pos.y);
                pos2 = new int2(pos.x - 1, pos.y);
                break;
            case 270:
                output = new int2(pos.x, pos.y - 2);
                pos2 = new int2(pos.x, pos.y - 1);
                break;
        }
        var smelterGameObject = Instantiate(smelter, new Vector3(pos.x, pos.y, 0f), rotation);
        var outpuProcessingBuilding = smelterGameObject.GetComponent<OutputProcessingBuilding>();
        outpuProcessingBuilding.SetPosition(output);
        var processingBuilding = smelterGameObject.GetComponent<ProcessingBuilding>();
        processingBuilding.SetRecipe(recipe);
        SetPositions(smelter, new []{pos, pos2});
        var inputs = smelterGameObject.GetComponents<InputProcessingBuilding>();
        inputs[0].SetPosition(pos);
        return smelter;
    }

    public GameObject SpawnAssembler(int2 pos, Quaternion rotation, Recipe recipe)
    {
        int2 output = int2.zero, input1 = int2.zero, input2 = int2.zero, pos2 = int2.zero, pos3 = int2.zero;
        switch (rotation.eulerAngles.z)
        {
            case 0:
                output = new int2(pos.x + 2, pos.y);
                input1 = new int2(pos.x - 1, pos.y + 1);
                input2 = new int2(pos.x - 1, pos.y - 1);
                pos2 = new int2(pos.x + 1, pos.y + 1);
                pos3 = new int2(pos.x + 1, pos.y - 1);
                break;
            case 90:
                output = new int2(pos.x, pos.y + 2);
                input1 = new int2(pos.x + 1, pos.y - 1);
                input2 = new int2(pos.x - 1, pos.y - 1);
                pos2 = new int2(pos.x + 1, pos.y + 1);
                pos3 = new int2(pos.x - 1, pos.y + 1);
                break;
            case 180:
                output = new int2(pos.x - 2, pos.y);
                input1 = new int2(pos.x + 1, pos.y + 1);
                input2 = new int2(pos.x + 1, pos.y - 1);
                pos2 = new int2(pos.x - 1, pos.y + 1);
                pos3 = new int2(pos.x - 1, pos.y - 1);
                break;
            case 270:
                output = new int2(pos.x, pos.y - 2);
                input1 = new int2(pos.x + 1, pos.y + 1);
                input2 = new int2(pos.x - 1, pos.y + 1);
                pos2 = new int2(pos.x + 1, pos.y - 1);
                pos3 = new int2(pos.x - 1, pos.y - 1);
                break;
        }
        var assemblerGameObject = Instantiate(assembler, new Vector3(pos.x, pos.y, 0f), rotation);
        var outpuProcessingBuilding = assemblerGameObject.GetComponent<OutputProcessingBuilding>();
        outpuProcessingBuilding.SetPosition(output);
        var processingBuilding = assemblerGameObject.GetComponent<ProcessingBuilding>();
        processingBuilding.SetRecipe(recipe);
        SetPositions(assembler, new []{pos, input1, input2, pos2, pos3, new int2(pos.x, pos.y + 1), new int2(pos.x, pos.y - 1), new int2(pos.x + 1, pos.y), new int2(pos.x - 1, pos.y)});
        var inputs = assemblerGameObject.GetComponents<InputProcessingBuilding>();
        inputs[0].SetPosition(input1);
        inputs[1].SetPosition(input2);
        return assembler;
    }
    
    public GameObject SpawnTrashcan(int2 pos)
    {
        var trashcanGameObject = Instantiate(trashcan, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        var inputs = trashcanGameObject.GetComponents<InputProcessingBuilding>();
        inputs[0].SetPosition(pos);
        SetPositions(trashcan, new []{pos});
        return trashcan;
    }

    private void SetPositions(GameObject gameObject, int2[] pos)
    {
        var positions = gameObject.GetComponent<Position>();
        positions.SetPositions(pos);
    }
}