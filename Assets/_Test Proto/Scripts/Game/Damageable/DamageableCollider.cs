using UnityEngine;
using Zenject;

namespace TestProto
{
	[RequireComponent(typeof(CapsuleCollider))]
	public class DamageableCollider : MonoBehaviour, IDamageableCollider
	{
		[Inject] private IDamageable _damageable;
		
		private CapsuleCollider _collider;
		
		private void Awake() => _collider = GetComponent<CapsuleCollider>();
		
		public void Disable() => _collider.enabled = false;

		public void Enable() => _collider.enabled = true;

		public void TakeDamage(float damage) => _damageable.TryTakeDamage(damage);
	}
}