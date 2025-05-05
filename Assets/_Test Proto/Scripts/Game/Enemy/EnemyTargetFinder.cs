using TestProto.Players;
using UnityEngine;
using System;

namespace TestProto.Enemies
{
	public class EnemyTargetFinder : MonoBehaviour
	{
		public IReadOnlyPlayerCar CurrentTarget { get; private set; }
		
		public bool IsHasTarget => CurrentTarget != null;
		
		public event Action OnTargetFound;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IReadOnlyPlayerCar car))
			{
				CurrentTarget = car;
				OnTargetFound?.Invoke();
			}
		}
	}
}