using UnityEngine;

namespace TestProto
{
	public abstract class StateMachine : MonoBehaviour
	{
		private State _requestedState;

		public State CurrentState { get; protected set; }
		public State LastState { get; protected set; }

		public abstract void Initialize();

		public void RequestSwitchStateTo(State state)
		{
			_requestedState = state;
		}

		protected virtual void TrySwitchStateTo(State nextState)
		{
			if (nextState != CurrentState)
			{
				LastState = CurrentState;
				CurrentState?.Exit();
				CurrentState = nextState;
				CurrentState?.Enter();
			}
		}

		protected virtual void Update()
		{
			CurrentState?.Perform();
			
			if (_requestedState != null)
			{
				TrySwitchStateTo(_requestedState);
				_requestedState = null;
			}
			else if (CurrentState != null && CurrentState.IsReadyToTransit)
			{
				TrySwitchStateTo(CurrentState.NextState);
			}
		}
	}

}