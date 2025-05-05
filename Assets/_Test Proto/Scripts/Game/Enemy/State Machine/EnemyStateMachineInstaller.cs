using TestProto.Enemies.States;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyStateMachineInstaller : MonoInstaller
	{
		[SerializeField] private EnemyStateMachine _stateMachine;
		[SerializeField] private Chase _chase;
		[SerializeField] private Idle _idle;

		public override void InstallBindings()
		{
			Container.BindInstance(_idle).WhenInjectedIntoInstance(_stateMachine);
			Container.BindInstance(_chase).WhenInjectedInto<EnemyState>();
			Container.BindInstance(_idle).WhenInjectedInto<EnemyState>();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_stateMachine = GetComponentInChildren<EnemyStateMachine>(true);
			_chase = GetComponentInChildren<Chase>(true);
			_idle = GetComponentInChildren<Idle>(true);
		}
#endif
	}
}