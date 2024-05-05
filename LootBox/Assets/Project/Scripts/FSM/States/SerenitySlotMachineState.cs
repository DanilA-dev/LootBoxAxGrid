using AxGrid.FSM;
using UnityEngine;

namespace States
{
    [State(nameof(SerenitySlotMachineState))]
    public class SerenitySlotMachineState : FSMState
    {
        
        [Enter]
        public void Enter()
        {
            Debug.Log("Enter serenity state");
        }

        [Exit]
        public void Exit()
        {
            Debug.Log("Exit serenity state");
        }
    }
}