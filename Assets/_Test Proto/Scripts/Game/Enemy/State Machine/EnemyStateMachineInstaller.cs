using TestProto.Enemies.States;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyStateMachineInstaller : MonoInstaller
	{
		[SerializeField] private EnemyStateMachine _stateMachine;
		[SerializeField] private Chase _chaseState;
		[SerializeField] private Idle _idleState;

		public override void InstallBindings()
		{
			BindIntoStateMachine();
			BindIntoEnemyState();
		}

		private void BindIntoStateMachine()
		{
			Container.BindInstance(_idleState).WhenInjectedIntoInstance(_stateMachine);
		}
		
		private void BindIntoEnemyState()
		{
			Container.BindInstance(_chaseState).WhenInjectedInto<EnemyState>();
			Container.BindInstance(_idleState).WhenInjectedInto<EnemyState>();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_stateMachine = GetComponentInChildren<EnemyStateMachine>(true);
			_chaseState = GetComponentInChildren<Chase>(true);
			_idleState = GetComponentInChildren<Idle>(true);
		}
#endif
	}
}