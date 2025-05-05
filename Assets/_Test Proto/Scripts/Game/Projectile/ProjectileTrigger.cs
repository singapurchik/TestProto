using UnityEngine;
using System;

namespace TestProto.Projectiles
{
	public class ProjectileTrigger : MonoBehaviour
	{
		public event Action<IDamageableCollider> OnTriggerTarget;
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageableCollider damageableCollider))
				OnTriggerTarget?.Invoke(damageableCollider);
		}
	}
}