using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Components
{
    public class BeltPath : MonoBehaviour
    {
        private List<ConveyorComponent> beltPath = new ();
        private InputConveyorComponent input;
        private OutputConveyorComponent output;
        private float timeToMove;

        public void Start()
        {
            input = GetComponent<InputConveyorComponent>();
            output = GetComponent<OutputConveyorComponent>();
            timeToMove = 2f;
        }

        public void Update()
        {
            timeToMove -= Time.deltaTime;
            if(timeToMove > 0) return;
            var lastBelt = beltPath[^1];
            if (!ReferenceEquals(lastBelt.item, null) && ReferenceEquals(output.GetItem(), null))
            {
                var item = lastBelt.item;
                var itemComponent = item.GetComponent<Item>();
                itemComponent.SetPosition(output.GetPosition());
                output.SetItem(item);
                lastBelt.item = null;
            }
            for (int i = beltPath.Count-2; i >= 0; i--)
            {
                var thisConveyor = beltPath[i];
                var lastConveyor = beltPath[i + 1];
                if (ReferenceEquals(thisConveyor.item, null)) continue;
                if (!ReferenceEquals(lastConveyor.item, null)) continue;
                var item = thisConveyor.item;
                var itemComponent = item.GetComponent<Item>();
                lastConveyor.item = item;
                itemComponent.SetPosition(lastConveyor.pos);
                thisConveyor.item = null;
            }
            var firstConveyor = beltPath[0];
            input.SetOccupied(!ReferenceEquals(firstConveyor.item, null));
            if (!ReferenceEquals(input.GetItem(), null) && ReferenceEquals(firstConveyor.item, null))
            {
                firstConveyor.item = input.GetItem();
                input.RemoveItem();
            }
            timeToMove += 2f;
        }

        public void AddConveyor(ConveyorComponent conveyorComponent)
        {
            beltPath.Add(conveyorComponent);
        }
    }
}