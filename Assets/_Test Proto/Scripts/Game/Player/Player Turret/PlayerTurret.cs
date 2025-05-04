using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class PlayerTurret : MonoBehaviour
	{
		[SerializeField] private TurretAttacker _attacker;
		[SerializeField] private TurretRotator _rotator;
		
		[Inject] private IReadOnlyPlayerInput _input;
		
		private const float MIN_INPUT_STEP_TO_ROTATE = 0.01f;

		[Inject]
		private void Construct(DiContainer container) => container.Inject(_attacker);
		
		private void Awake() => _rotator.Initialize(transform.localEulerAngles.z);
		
		public void TryAttack() => _attacker.TryAttack();

		public void Rotate()
		{
			if (Mathf.Abs(_input.HorizontalInput) > MIN_INPUT_STEP_TO_ROTATE)
				_rotator.Rotate(transform, _input.HorizontalInput);
		}
	}
}