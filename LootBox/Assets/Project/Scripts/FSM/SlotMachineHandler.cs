using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using Core.Interfaces;
using Core.Types;
using Model;
using States;
using UnityEngine;

namespace Core
{
    public class SlotMachineHandler : MonoBehaviourExtBind
    {
        private const int SLOT_AMOUNT = 3;
        private readonly string _startState = nameof(InitSlotsState);

        private SlotMachineModel _slotMachine;
        
        private bool _canStopScroll;
        
        private bool AllowStartScroll 
            => Settings.Fsm.CurrentStateName != nameof(ScrollSlotsState);

        private bool AllowStopScroll
            => Settings.Fsm.CurrentStateName == nameof(ScrollSlotsState) && _canStopScroll;
             
        [OnAwake]
        private void InitFSM()
        {
            _slotMachine = new SlotMachineModel(SLOT_AMOUNT);
            
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new InitSlotsState());
            Settings.Fsm.Add(new SetItemSlotState(SLOT_AMOUNT));
            Settings.Fsm.Add(new ScrollSlotsState());
        }

        [OnDelay(0.1f)]
        private void StartFSM()
        {
            Settings.Model.EventManager.AddAction<ButtonBindType>(Constants.Events.BUTTON_CLICK, OnSlotStartCallBack);
            Settings.Model.EventManager.AddAction(Constants.Events.SLOTS_CAN_STOP, UpdateStopButtonState);
            Settings.Fsm.Start(_startState);
        }
        
        [OnUpdate]
        private void OnUpdate()
        {
            Settings.Fsm.Update(Time.deltaTime);  
        }

        [OnDestroy]
        private void OnDestroy()
        {
            Settings.Model.EventManager.RemoveAction<ButtonBindType>(Constants.Events.BUTTON_CLICK, OnSlotStartCallBack);
            Settings.Model.EventManager.RemoveAction(Constants.Events.SLOTS_CAN_STOP, UpdateStopButtonState);
        }
        
        private void OnSlotStartCallBack(ButtonBindType buttonType)
        {
            if (buttonType == ButtonBindType.StartSlot)
            {
                if(!AllowStartScroll)
                    return;

                _canStopScroll = false;
                Settings.Fsm.Change(nameof(ScrollSlotsState));
                return;
            }

            if (buttonType == ButtonBindType.StopSlot)
            {
                if(!AllowStopScroll)
                    return;
                
                Settings.Fsm.Change(nameof(SetItemSlotState));
                return;
            }
            
            Debug.Log($"Can't find button name {buttonType}");
        }

        private void UpdateStopButtonState()
        {
            _canStopScroll = true;
        }
        
    }
}