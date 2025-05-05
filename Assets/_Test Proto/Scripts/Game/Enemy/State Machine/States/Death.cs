using UnityEngine;
using Zenject;

namespace TestProto.Enemies.States
{
	public class Death : EnemyState
	{
		[Inject] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[Inject] private DamageableCollider _damageableCollider;
		
		public override void Enter()
		{
			_skinnedMeshRenderer.enabled = false;
			_damageableCollider.Disable();
		}

		public override void Perform()
		{
		}

		public override void Exit()
		{
			_skinnedMeshRenderer.enabled = true;
			_damageableCollider.Enable();
			base.Exit();
		}
	}
}