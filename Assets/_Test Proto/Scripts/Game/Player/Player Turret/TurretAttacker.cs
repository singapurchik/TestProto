using TestProto.Projectiles;
using UnityEngine;
using Zenject;

namespace TestProto.Players.Turrets
{
	public class TurretAttacker : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _attackEffect;
		[SerializeField] private Transform _projectileSpawnPoint;
		[SerializeField] private float _damage = 50f;
		[SerializeField] private float _delay = 1f;

		[Inject] private ProjectilePool _pool;

		private float _nextAttackTime;

		public void TryAttack()
		{
			if (Time.timeSinceLevelLoad > _nextAttackTime)
				Attack();
		}

		private void Attack()
		{
			_attackEffect.Play();

			var bullet = _pool.Get();
			bullet.transform.SetPositionAndRotation(_projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
			bullet.Launch(bullet.transform.position + bullet.transform.forward, _damage);
			
			_nextAttackTime = Time.timeSinceLevelLoad + _delay;
		}
	}
}