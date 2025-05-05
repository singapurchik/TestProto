using UnityEngine;
using Zenject;
using System;

namespace TestProto.Enemies
{
	public class Enemy : MonoBehaviour, IEnemyEvents
	{
		[Inject] private EnemyStateMachine _stateMachine;
		[Inject] private Health _health;

		public event Action<Enemy> OnDead;

		public void Initialize()
		{
			_health.TryHeal(float.MaxValue);
			_stateMachine.Initialize();
		}

		public void Kill() => _health.TryTakeDamage(float.MaxValue);
		
		public void InvokeOnDead() => OnDead?.Invoke(this);
	}
}