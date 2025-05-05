using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class PlayerCar : MonoBehaviour, IReadOnlyPlayerCar
	{
		[Inject] private PlayerCarAnimator _animator;
		[Inject] private PlayerCarMover _mover;
		[Inject] private Health _health;

		private bool _isMoveRequested;
		
		public Vector3 Position => transform.position;

		private void OnEnable()
		{
			_health.OnTakeDamage += OnTakeDamage;
		}

		private void OnDisable()
		{
			_health.OnTakeDamage -= OnTakeDamage;
		}

		private void OnTakeDamage()
		{
			_animator.PlayTakeDamageAnim();
		}

		public void RequestMove() => _isMoveRequested = true;

		private void Update()
		{
			if (_isMoveRequested)
			{
				_mover.Move();
				_isMoveRequested = false;
			}
		}
	}
}