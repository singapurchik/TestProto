using TestProto.Enemies.States;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Enemies
{
	public class EnemyStateMachineInstaller : MonoInstaller
	{
		[SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[SerializeField] private DamageableCollider _damageableCollider;
		[SerializeField] private EnemyStateMachine _stateMachine;
		[SerializeField] private EnemyDamageDealer _damageDealer;
		[SerializeField] private Death _deathState;
		[SerializeField] private Chase _chaseState;
		[SerializeField] private Idle _idleState;

		public override void InstallBindings()
		{
			BindIntoStateMachine();
			BindIntoDeathState();
			BindIntoEnemyState();
		}

		private void BindIntoStateMachine()
		{
			Container.BindInstance(_idleState).WhenInjectedIntoInstance(_stateMachine);
		}

		private void BindIntoDeathState()
		{
			Container.BindInstance(_skinnedMeshRenderer).WhenInjectedIntoInstance(_deathState);
			Container.BindInstance(_damageableCollider).WhenInjectedIntoInstance(_deathState);
			Container.BindInstance(_damageDealer).WhenInjectedIntoInstance(_deathState);
		}
		
		private void BindIntoEnemyState()
		{
			Container.BindInstance(_deathState).WhenInjectedInto<EnemyState>();
			Container.BindInstance(_chaseState).WhenInjectedInto<EnemyState>();
			Container.BindInstance(_idleState).WhenInjectedInto<EnemyState>();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_skinnedMeshRenderer = transform.parent.GetComponentInChildren<SkinnedMeshRenderer>(true);
			_damageableCollider = transform.parent.GetComponentInChildren<DamageableCollider>(true);
			_damageDealer = transform.parent.GetComponentInChildren<EnemyDamageDealer>(true);
			_stateMachine = GetComponentInChildren<EnemyStateMachine>(true);
			_deathState = GetComponentInChildren<Death>(true);
			_chaseState = GetComponentInChildren<Chase>(true);
			_idleState = GetComponentInChildren<Idle>(true);
		}
#endif
	}
}