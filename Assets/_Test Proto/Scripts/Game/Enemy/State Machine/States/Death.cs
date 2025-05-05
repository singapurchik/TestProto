using UnityEngine;
using Zenject;

namespace TestProto.Enemies.States
{
	public class Death : EnemyState
	{
		[Inject] private SkinnedMeshRenderer _skinnedMeshRenderer;
		[Inject] private DamageableCollider _damageableCollider;
		[Inject] private EnemyDamageDealer _damageDealer;
		
		public override void Enter()
		{
			_skinnedMeshRenderer.enabled = false;
			_damageableCollider.Disable();
			_damageDealer.Disable();
		}

		public override void Perform()
		{
		}

		public override void Exit()
		{
			_skinnedMeshRenderer.enabled = true;
			_damageableCollider.Enable();
			_damageDealer.Enable();
			base.Exit();
		}
	}
}