namespace TestProto.Enemies.States
{
	public class Chase : EnemyState
	{
		public override void Enter()
		{
			Animator.UpdateLocomotionAnim(Mover.MoveSpeedNormalized);
		}

		public override void Perform()
		{
			if (TargetFinder.IsHasTarget)
			{
				var targetPosition = TargetFinder.CurrentTarget.Position;
				Rotator.RequestRotateTo(targetPosition);
				Mover.RequestMoveTo(targetPosition);
				Animator.UpdateLocomotionAnim(Mover.MoveSpeedNormalized);
			}
			else
			{
				RequestTransition(IdleSate);
			}
		}
	}
}