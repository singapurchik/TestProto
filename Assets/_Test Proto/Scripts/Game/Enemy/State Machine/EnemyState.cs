using Zenject;

namespace TestProto.Enemies.States
{
	 public abstract class EnemyState : State
	 { 
		[Inject] protected IReadOnlyTargetFinder TargetFinder;
		[Inject] protected IReadOnlyHealth Health;
		[Inject] protected EnemyAnimator Animator;
		[Inject] protected EnemyRotator Rotator;
		[Inject] protected EnemyMover Mover;
		
		[Inject] protected Chase ChaseState;
		[Inject] protected Idle IdleSate;
		
		private void OnEnable()
		{
			Health.OnTakeDamage += OnTakeDamage;
		}

		private void OnDisable()
		{
			Health.OnTakeDamage -= OnTakeDamage;
		}
		
		protected virtual void OnTakeDamage()
		{
			Animator.PlayTakeDamageAnim();
		}
	 }
}
