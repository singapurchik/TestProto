using UnityEngine;

namespace TestProto.Players
{
	public class PlayerCarRotator : MonoBehaviour
	{
		[SerializeField] private Transform _body;
		[SerializeField] private float _maxSteerAngle = 3f;
		[SerializeField] private float _returnSpeed = 90f;
		
		private float _defaultY;

		private void Awake()
		{
			_defaultY = _body.localEulerAngles.y;
		}

		public void UpdateRotation(float currentVelocity, float maxVelocity)
		{
			var steerPercent = Mathf.Clamp(currentVelocity / maxVelocity, -1f, 1f);
			var targetY = _defaultY + steerPercent * _maxSteerAngle;

			var currentRotation = _body.localRotation;
			var targetRotation = Quaternion.Euler(0f, targetY, 0f);

			_body.localRotation = Quaternion.RotateTowards(
				currentRotation, targetRotation, _returnSpeed * Time.deltaTime);
		}
	}
}