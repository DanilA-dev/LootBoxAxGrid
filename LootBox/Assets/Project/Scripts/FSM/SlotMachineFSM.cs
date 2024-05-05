using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using States;
using UnityEngine;

namespace Core
{
    public class SlotMachineFSM : MonoBehaviourExtBind
    {
        private readonly string _startState = nameof(SerenitySlotMachineState);

        private bool _canStopScroll;
        private bool CanStartScroll 
            => Settings.Fsm.CurrentStateName != nameof(ScrollSlotMachineState);

        
        [OnAwake]
        private void InitFSM()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new SerenitySlotMachineState());
            Settings.Fsm.Add(new ScrollSlotMachineState());
        }

        [OnStart]
        private void StartFSM()
        {
            Settings.Fsm.Start(_startState);
            Settings.Model.EventManager.AddAction<string>("OnBtn", OnSlotStartCallBack);
            Settings.Model.EventManager.AddAction("CanBeStopped", UpdateStopButtonState);
        }
        
        [OnUpdate]
        private void OnUpdate()
        {
            Settings.Fsm.Update(Time.deltaTime);  
        }

        [OnDestroy]
        private void OnDestroy()
        {
            Settings.Model.EventManager.RemoveAction<string>("OnBtn", OnSlotStartCallBack);
            Settings.Model.EventManager.RemoveAction("CanBeStopped", UpdateStopButtonState);
        }
        
        //string based button, bad
        public void OnSlotStartCallBack(string buttonName)
        {
            if (buttonName == "StartScroll")
            {
                if(!CanStartScroll)
                    return;

                _canStopScroll = false;
                Settings.Fsm.Change(nameof(ScrollSlotMachineState));
                return;
            }

            if (buttonName == "StopScroll")
            {
                if(!_canStopScroll)
                    return;
                
                if(Settings.Fsm.CurrentStateName == nameof(SerenitySlotMachineState))
                    return;
                
                Settings.Fsm.Change(nameof(SerenitySlotMachineState));
                return;
            }
            
            Debug.Log($"Can't find button name {buttonName}");
        }

        private void UpdateStopButtonState()
        {
            _canStopScroll = true;
        }
        
    }
}