using System;
using System.Collections.Generic;
using AxGrid;
using Core.Interfaces;

namespace Model
{
    public class SlotMachineModel
    {
        public IDictionary<int, ISlotModel> Slots { get; private set; }
        
        public SlotMachineModel(int slotAmount)
        {
            Slots = new Dictionary<int, ISlotModel>();
            
            for (int i = 0; i < slotAmount; i++)
                Slots.Add(i, new SlotModel());
            
            Settings.Model.Set(Constants.Models.SLOTMACHINE_MODEL, this);

        }

        public ISlotModel GetSlot(int index)
        {
            if(Slots.TryGetValue(index, out var slot))
                return slot;
            
            throw new ArgumentException($"Slot with index {index} doesn't exist");
        }
        
    }
}