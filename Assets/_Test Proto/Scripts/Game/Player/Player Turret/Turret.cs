using UnityEngine;
using Zenject;

namespace TestProto.Players.Turrets
{
	public class Turret : MonoBehaviour
	{
		[Inject] private IReadOnlyPlayerInput _input;
		[Inject] private TurretAttacker _attacker;
		[Inject] private TurretRotator _rotator;
		
		private bool _isActive;

		private const float MIN_INPUT_STEP_TO_ROTATE = 0.01f;

		public void Disable() => _isActive = false;
		
		public void Enable() => _isActive = true;

		private void Update()
		{
			if (_isActive)
			{
				if (Mathf.Abs(_input.HorizontalInput) > MIN_INPUT_STEP_TO_ROTATE)
					_rotator.Rotate(_input.HorizontalInput);
				
				_attacker.TryAttack();
			}
		}
	}
}