namespace TestProto.Enemies.States
{
	public class Chase : EnemyState
	{
		public override void Enter()
		{
		}

		public override void Perform()
		{
			if (TargetFinder.IsHasTarget)
			{
				var targetPosition = TargetFinder.CurrentTarget.Position;
				Rotator.RequestRotateTo(targetPosition);
				Mover.RequestRunTo(targetPosition);
			}
			else
			{
				RequestTransition(IdleSate);
			}
		}
	}
}