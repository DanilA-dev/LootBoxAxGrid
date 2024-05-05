using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

namespace States
{
    [State(nameof(ScrollSlotMachineState))]
    public class ScrollSlotMachineState : FSMState
    {
        private const float STOP_DELAY = 3f;
        
        [Enter]
        public void Enter()
        {
            Debug.Log("Enter scroll state");
        }

        [One(STOP_DELAY)]
        public void OnDelay()
        {
            Settings.Invoke("CanBeStopped");
        }
        
        [Exit]
        public void Exit()
        {
            Debug.Log("Exit scroll state");
        }
        
    }
}