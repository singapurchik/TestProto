using UnityEngine;
using Zenject;
using System;

namespace TestProto.Enemies
{
	public class Enemy : MonoBehaviour
	{
		[Inject] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[Inject] private DamageableCollider _damageableCollider;
		[Inject] private EnemyDamageDealer _damageDealer;
		[Inject] private EnemyStateMachine _stateMachine;
		[Inject] private EnemyTargetFinder _targetFinder;
		[Inject] private Health _health;
		
		public event Action<Enemy> OnDead;

		private void OnEnable()
		{
			_health.OnZeroHealth += OnZeroHealth;
		}

		private void OnDisable()
		{
			_health.OnZeroHealth -= OnZeroHealth;
		}

		public void Initialize()
		{
			_health.TryHeal(float.MaxValue);
			_skinnedMeshRenderer.enabled = true;
			_damageableCollider.Enable();
			_damageDealer.Enable();
			_targetFinder.Enable();
			_stateMachine.Initialize();
		}

		private void OnZeroHealth()
		{
			_skinnedMeshRenderer.enabled = false;
			_damageableCollider.Disable();
			_damageDealer.Disable();
			_targetFinder.Disable();
			_targetFinder.LoseTarget();
			OnDead?.Invoke(this);
		}
		
		public void Kill() => _health.TryTakeDamage(float.MaxValue);
	}
}