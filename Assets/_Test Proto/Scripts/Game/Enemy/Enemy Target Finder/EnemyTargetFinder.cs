using TestProto.Players;
using UnityEngine;
using System;

namespace TestProto.Enemies
{
	[RequireComponent(typeof(SphereCollider))]
	public class EnemyTargetFinder : MonoBehaviour, IReadOnlyTargetFinder
	{
		private SphereCollider _collider;
		
		public IReadOnlyPlayerCar CurrentTarget { get; private set; }
		
		public bool IsHasTarget => CurrentTarget != null;
		
		public event Action OnTargetFound;

		private void Awake()
		{
			_collider = GetComponent<SphereCollider>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IReadOnlyPlayerCar car))
			{
				CurrentTarget = car;
				OnTargetFound?.Invoke();
			}
		}
		
		public void Disable() => _collider.enabled = false;
		
		public void Enable() => _collider.enabled = true;
		
		public void LoseTarget() => CurrentTarget = null;
	}
}