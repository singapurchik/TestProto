using UnityEngine;

namespace TestProto.Enemies.States
{
	public class Idle : EnemyState
	{
		public override void Enter()
		{
		}

		public override void Perform()
		{
			if (TargetFinder.IsHasTarget)
			{
				Rotator.RequestRotateTo(TargetFinder.CurrentTarget.Position);
				
				if (Rotator.IsLookingAtTarget)
					RequestTransition(ChaseState);
			}
		}
	}
}