using Unity.Mathematics;
using UnityEngine;

namespace Buildings.Components
{
    public class OutputConveyorComponent : MonoBehaviour
    {
        private GameObject item;
        private int2 pos;
        private GameObject output;

        void Start()
        {
            item = null;
            output = null;
        }

        private void Update()
        {
            if (ReferenceEquals(item, null)) return;
            if (!ReferenceEquals(output, null))
            {
                var inputs = output.GetComponents<InputProcessingBuilding>();
                foreach (var input in inputs)
                {
                    if (!input.GetPosition().Equals(pos)) continue;
                    if (input.IsOccupied() || !input.GetItem().Equals(-1)) return;
                    var itemID = item.GetComponent<Item>().GetItemID();
                    Destroy(item);
                    item = null;
                    input.SetItem(itemID);
                }
            }
            else
            {
                output = BuildingDictionary.Instance.GetGameObjectAtPosition(pos);
            }
        }

        public void SetPosition(int2 pos)
        {
            this.pos = pos;
        }

        public int2 GetPosition()
        {
            return pos;
        }

        public void SetItem(GameObject item)
        {
            this.item = item;
        }

        public GameObject GetItem()
        {
            return item;
        }

        public void RemoveItem()
        {
            item = null;
        }
    }
}