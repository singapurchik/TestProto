using Zenject;

namespace TestProto.Enemies.States
{
	 public abstract class EnemyState : State
	 { 
		[Inject] protected EnemyTargetFinder TargetFinder;
		[Inject] protected EnemyRotator Rotator;
		[Inject] protected EnemyMover Mover;
		
		[Inject] protected Chase ChaseState;
		[Inject] protected Idle IdleSate;
	}
}
