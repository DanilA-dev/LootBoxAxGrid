using AxGrid;
using AxGrid.FSM;
using UnityEngine;

namespace States
{
    [State(nameof(InitSlotsState))]
    public class InitSlotsState : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Initial State");
            Settings.Invoke(Constants.Events.SLOTS_INIT);
        }
    }
}