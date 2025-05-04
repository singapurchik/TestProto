using UnityEngine;

namespace TestProto.Players.Turrets
{
	public class TurretRotator : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed = 0.2f;
		[SerializeField] private float _minZ = -40f;
		[SerializeField] private float _maxZ = 40f;
		[SerializeField] private bool _isInverted;

		private float _currentAngleZ;

		private void Awake()
		{
			_currentAngleZ = transform.localEulerAngles.z;
			
			if (_currentAngleZ > 180f)
				_currentAngleZ -= 360f;
		}
		
		public void Rotate(float delta)
		{
			var direction = _isInverted ? 1f : -1f;
			_currentAngleZ = Mathf.Clamp(_currentAngleZ + delta * _rotationSpeed * direction, _minZ, _maxZ);
			transform.localRotation = Quaternion.Euler(-90f, 0f, _currentAngleZ);
		}
	}
}