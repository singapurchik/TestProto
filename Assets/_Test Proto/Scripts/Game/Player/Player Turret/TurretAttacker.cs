using TestProto.Projectiles;
using UnityEngine;
using Zenject;

namespace TestProto.Players.Turrets
{
	public class TurretAttacker : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _attackEffect;
		[SerializeField] private Transform _projectileSpawnPoint;
		[SerializeField] private float _range = 10f;
		[SerializeField] private float _damage = 50f;

		[Inject] private ProjectilePool _pool;

		public void Attack()
		{
			_attackEffect.Play();
			var bullet = _pool.Get();
			bullet.transform.SetPositionAndRotation(_projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
			bullet.Launch(bullet.transform.position + bullet.transform.forward * _range, _damage);
		}
	}
}