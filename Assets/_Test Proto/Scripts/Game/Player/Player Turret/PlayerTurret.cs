using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class PlayerTurret : MonoBehaviour
	{
		[SerializeField] private TurretAttacker _attacker;
		[SerializeField] private TurretRotator _rotator;
		[SerializeField] private GameObject _laser;
		
		[Inject] private IReadOnlyPlayerInput _input;

		private bool _isRotateRequested;
		private bool _isAttackRequested;
		

		[Inject]
		private void Construct(DiContainer container) => container.Inject(_attacker);
		
		private void Awake() => _rotator.Initialize(transform.localEulerAngles.z);

		public void RequestAttack() => _isAttackRequested = true;

		public void RequestRotate() => _isRotateRequested = true;
		
		public void HideLaser() => _laser.SetActive(false);

		private void Rotate()
		{
			if (_input.IsHorizontalInputProcess)
				_rotator.Rotate(transform, _input.HorizontalInput);
		}

		private void Update()
		{
			if (_isAttackRequested)
			{
				_attacker.TryAttack();
				_isAttackRequested = false;
			}

			if (_isRotateRequested)
			{
				Rotate();
				_isRotateRequested = false;
			}
		}
	}
}