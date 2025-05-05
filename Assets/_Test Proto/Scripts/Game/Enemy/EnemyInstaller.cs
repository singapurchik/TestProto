using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyInstaller : MonoInstaller
	{
		[SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[SerializeField] private DamageableCollider _damageableCollider;
		[SerializeField] private EnemyVisualEffects _visualEffects;
		[SerializeField] private EnemyStateMachine _stateMachine;
		[SerializeField] private EnemyTargetFinder _targetFinder;
		[SerializeField] private EnemyDamageDealer _damageDealer;
		[SerializeField] private EnemyAnimator _enemyAnimator;
		[SerializeField] private EnemyRotator _rotator;
		[SerializeField] private Animator _animator;
		[SerializeField] private EnemyMover _mover;
		[SerializeField] private Health _health;
		[SerializeField] private Enemy _enemy;

		public override void InstallBindings()
		{
			BindFromInstance();
			BindIntoAnimator();
			BindIntoEnemy();
			BindInstance();
		}
		
		private void BindFromInstance()
		{
			Container.Bind<IReadOnlyTargetFinder>().FromInstance(_targetFinder).AsSingle();
			Container.Bind<IReadOnlyHealth>().FromInstance(_health).AsSingle();
			Container.Bind<IDamageable>().FromInstance(_health).AsSingle();
		}
		
		private void BindIntoEnemy()
		{
			Container.BindInstance(_skinnedMeshRenderer).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_damageableCollider).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_targetFinder).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_stateMachine).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_damageDealer).WhenInjectedIntoInstance(_enemy);
			Container.BindInstance(_health).WhenInjectedIntoInstance(_enemy);
		}

		private void BindIntoAnimator()
		{
			Container.BindInstance(_skinnedMeshRenderer).WhenInjectedIntoInstance(_enemyAnimator);
			Container.BindInstance(_animator).WhenInjectedIntoInstance(_enemyAnimator);
		}

		private void BindInstance()
		{
			Container.BindInstance(_visualEffects).AsSingle();
			Container.BindInstance(_enemyAnimator).AsSingle();
			Container.BindInstance(_rotator).AsSingle();
			Container.BindInstance(_mover).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
			_damageableCollider = GetComponentInChildren<DamageableCollider>(true);
			_visualEffects = GetComponentInChildren<EnemyVisualEffects>(true);
			_damageDealer = GetComponentInChildren<EnemyDamageDealer>(true);
			_stateMachine = GetComponentInChildren<EnemyStateMachine>(true);
			_targetFinder = GetComponentInChildren<EnemyTargetFinder>(true);
			_enemyAnimator = GetComponentInChildren<EnemyAnimator>(true);
			_rotator = GetComponentInChildren<EnemyRotator>(true);
			_animator = GetComponentInChildren<Animator>(true);
			_mover = GetComponentInChildren<EnemyMover>(true);
			_health = GetComponentInChildren<Health>(true);
			_enemy = GetComponentInChildren<Enemy>(true);
		}
#endif
	}
}