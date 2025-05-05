using UnityEngine;
using Zenject;

namespace TestProto.Enemies
{
	[RequireComponent(typeof(CapsuleCollider))]
	public class EnemyDamageDealer : MonoBehaviour
	{
		[SerializeField] private float _damage = 10f;
		
		[Inject] private IDamageable _damageable;
		
		private CapsuleCollider _collider;

		private void Awake() => _collider = GetComponent<CapsuleCollider>();
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageable targetDamageable))
				TakeDamage(targetDamageable);
		}

		private void TakeDamage(IDamageable targetDamageable)
		{
			_damageable.TryTakeDamage(float.MaxValue);
			targetDamageable.TryTakeDamage(_damage);
		}

		public void Disable() => _collider.enabled = false;
		
		public void Enable() => _collider.enabled = true;
	}
}