using AxGrid;
using AxGrid.FSM;

namespace States
{
    [State(nameof(ScrollSlotsState))]
    public class ScrollSlotsState : FSMState
    {
        private const float STOP_DELAY = 3f;
        
        [Enter]
        private void Enter()
        {
            Settings.Invoke(Constants.Events.SLOTS_SCROLL);
        }

        [One(STOP_DELAY)]
        public void OnDelay()
        {
            Settings.Invoke(Constants.Events.SLOTS_CAN_STOP);
        }
        
    }
}