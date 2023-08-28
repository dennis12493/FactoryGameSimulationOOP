using Unity.Mathematics;
using UnityEngine;

namespace Buildings.Components
{
    public class InputConveyorComponent : MonoBehaviour
    {
        private GameObject item;
        private bool occupied;
        private int2 pos;

        public void Start()
        {
            item = null;
            occupied = false;
            BuildingDictionary.Instance.InsertGameObjectInDictionary(new []{pos}, gameObject);
        }

        public void SetPosition(int2 pos)
        {
            this.pos = pos;
        }

        public void SetOccupied(bool occupied)
        {
            this.occupied = occupied;
        }

        public GameObject GetItem()
        {
            return item;
        }

        public void RemoveItem()
        {
            item = null;
        }

        public bool IsOccupied()
        {
            return occupied;
        }

        public void SetItem(GameObject item)
        {
            this.item = item;
            occupied = true;
        }
    }
}