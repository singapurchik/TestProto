using UnityEngine;
using Zenject;

namespace TestProto.Enemies.States
{
	 public abstract class EnemyState : State
	 { 
		[Inject] protected SkinnedMeshRenderer SkinnedMeshRenderer;
		[Inject] protected DamageableCollider DamageableCollider;
		[Inject] protected EnemyDamageDealer DamageDealer;
		[Inject] protected EnemyTargetFinder TargetFinder;
		[Inject] protected IReadOnlyHealth Health;
		[Inject] protected EnemyAnimator Animator;
		[Inject] protected EnemyRotator Rotator;
		[Inject] protected IEnemyEvents Events;
		[Inject] protected EnemyMover Mover;
		
		[Inject] protected Death DeathState;
		[Inject] protected Chase ChaseState;
		[Inject] protected Idle IdleSate;
		
		private void OnEnable()
		{
			Health.OnTakeDamage += OnTakeDamage;
			Health.OnZeroHealth += OnDead;
		}

		private void OnDisable()
		{
			Health.OnTakeDamage -= OnTakeDamage;
			Health.OnZeroHealth -= OnDead;
		}
		
		protected virtual void OnTakeDamage()
		{
			Animator.PlayTakeDamageAnim();
		}

		protected virtual void OnDead() => RequestTransition(DeathState);
	 }
}
