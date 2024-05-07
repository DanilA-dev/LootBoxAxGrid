using AxGrid;
using AxGrid.Base;
using AxGrid.Utils;
using Core.Interfaces;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISlotMachine : MonoBehaviourExtBind
    {
        
        [SerializeField] private UISlotItem[] _slotItems;
        [Space] 
        [SerializeField] private Button _startScrollButton;
        [SerializeField] private Button _stopScrollButton;

        [OnStart]
        private void OnStart()
        {
            Settings.Model.EventManager.AddAction(Constants.Events.SLOTS_INIT, InitUISlots);
            Settings.Model.EventManager.AddAction(Constants.Events.SLOTS_SCROLL, StartScroll);
            Settings.Model.EventManager.AddAction<SlotMachineModel>(Constants.Events.SLOTS_SET_ITEM, SetItemsToSlots);
            Settings.Model.EventManager.AddAction(Constants.Events.SLOTS_CAN_STOP, () => EnableButton(_stopScrollButton));
        }


        [OnDestroy]
        private void OnDestroy()
        {
            Settings.Model.EventManager.RemoveAction<SlotMachineModel>(Constants.Events.SLOTS_SET_ITEM, SetItemsToSlots);
            Settings.Model.EventManager.RemoveAction(Constants.Events.SLOTS_INIT, InitUISlots);
            Settings.Model.EventManager.RemoveAction(Constants.Events.SLOTS_SCROLL, StartScroll);
            Settings.Model.EventManager.RemoveAction(Constants.Events.SLOTS_CAN_STOP, () => EnableButton(_stopScrollButton));
        }
        
        private void InitUISlots()
        {
            var items = Settings.Model.Get<ItemsModel>(Constants.Models.ITEMS_MODEL);
            _slotItems.ForEach(i => i.Init(items));
            DisableButton(_stopScrollButton);
        }
        
        private void StartScroll()
        {
            DisableButton(_startScrollButton);
            DisableButton(_stopScrollButton);
            _slotItems.ForEach(i => i.ScrollStart());
        }
        
        private void SetItemsToSlots(SlotMachineModel slotMachineModel)
        {
            EnableButton(_startScrollButton);
            DisableButton(_stopScrollButton);
            for (int i = 0; i < _slotItems.Length; i++)
            {
                var slot = slotMachineModel.GetSlot(i);
                _slotItems[i].SetMainSlot(slot);
            }
        }

        private void EnableButton(Button button) => button.interactable = true;
        private void DisableButton(Button button) => button.interactable = false;
    }
}