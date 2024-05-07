using AxGrid;
using AxGrid.FSM;
using Extensions;
using Model;
using UnityEngine;

namespace States
{
    [State(nameof(SetItemSlotState))]
    public class SetItemSlotState : FSMState
    {
        private int _slotAmount;
        private SlotMachineModel _slotMachineModel;
        
        public SetItemSlotState(int slotAmount)
        {
            _slotAmount = slotAmount;
        }
        
        [Enter]
        private void Enter()
        {
            SetRandomItems();
            Settings.Invoke(Constants.Events.SLOTS_SET_ITEM, _slotMachineModel);
        }

        private void SetRandomItems()
        {
            var itemsModel = Settings.Model.Get<ItemsModel>(Constants.Models.ITEMS_MODEL);
            _slotMachineModel = Settings.Model.Get<SlotMachineModel>(Constants.Models.SLOTMACHINE_MODEL);
            for (int i = 0; i < _slotAmount; i++)
            {
                var randItem = itemsModel.Items.GetRandomElement();
                var slot = _slotMachineModel.GetSlot(i);
                slot.Item = randItem;
                Debug.Log($"Slot {i} has item {slot.Item.ItemName}");
            }
        }
    }
}