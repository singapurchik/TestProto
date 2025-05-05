using TestProto.Enemies.States;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyStateMachine : StateMachine
	{
		[Inject] private Idle _idleState;
		
		public override void Initialize() => TrySwitchStateTo(_idleState);
	}
}