using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyInstaller : MonoInstaller
	{
		[SerializeField] private EnemyStateMachine _stateMachine;
		[SerializeField] private EnemyTargetFinder _targetFinder;
		[SerializeField] private EnemyRotator _rotator;
		[SerializeField] private EnemyMover _mover;
		[SerializeField] private Enemy _enemy;

		public override void InstallBindings()
		{
			Container.BindInstance(_stateMachine).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_targetFinder).AsSingle();
			Container.BindInstance(_rotator).AsSingle();
			Container.BindInstance(_mover).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_stateMachine = GetComponentInChildren<EnemyStateMachine>(true);
			_targetFinder = GetComponentInChildren<EnemyTargetFinder>(true);
			_rotator = GetComponentInChildren<EnemyRotator>(true);
			_mover = GetComponentInChildren<EnemyMover>(true);
			_enemy = GetComponentInChildren<Enemy>(true);
		}
#endif
	}
}