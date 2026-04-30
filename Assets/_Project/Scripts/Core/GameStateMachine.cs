using System;

namespace TheLastSprout.Core
{
    public class GameStateMachine
    {
        public GameState CurrentState { get; private set; }
        public event Action<GameState, GameState> OnStateChanged;

        public GameStateMachine()
        {
            CurrentState = GameState.Booting;
        }

        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            var oldState = CurrentState;
            CurrentState = newState;
            
            OnStateChanged?.Invoke(oldState, newState);
        }
    }
}
