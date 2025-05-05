using UnityEngine;

namespace TestProto.Enemies.States
{
	public class Death : EnemyState
	{
		private float _timeToCompletelyDeadDelay;
		
		private const float COMPLETELY_DEAD_DELAY = 0;
		
		private bool _isCompletelyDead;
		
		public override void Enter()
		{
			SkinnedMeshRenderer.enabled = false;
			_isCompletelyDead = false;
			DamageableCollider.Disable();
			DamageDealer.Disable();
			_timeToCompletelyDeadDelay = Time.timeSinceLevelLoad + COMPLETELY_DEAD_DELAY;
		}

		public override void Perform()
		{
			if (!_isCompletelyDead && Time.timeSinceLevelLoad > _timeToCompletelyDeadDelay)
			{
				Events.InvokeOnDead();
				_isCompletelyDead = true;
			}
		}

		public override void Exit()
		{
			TargetFinder.LoseTarget();
			SkinnedMeshRenderer.enabled = true;
			DamageableCollider.Enable();
			DamageDealer.Enable();
			base.Exit();
		}
	}
}