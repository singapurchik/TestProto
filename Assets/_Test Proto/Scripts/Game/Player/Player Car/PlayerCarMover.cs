using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class PlayerCarMover : MonoBehaviour
	{
		[SerializeField] private float _forwardSpeed = 5f;
		[SerializeField] private float _maxHorizontalSpeed = 2f;
		[SerializeField] private float _acceleration = 4f;
		[SerializeField] private float _maxHorizontalOffset = 4f;
		[SerializeField] private float _minOffsetDelta = 0.5f;

		[Inject] private PlayerCarRotator _rotator;
		
		private float _currentX;
		private float _targetX;
		private float _velocityX;

		public void Move()
		{
			MoveForward();
			UpdateHorizontalPosition();
			UpdateRotation();
			CheckForNewTarget();
		}

		private void MoveForward()
		{
			transform.Translate(Vector3.forward * (_forwardSpeed * Time.deltaTime), Space.Self);
		}

		private void UpdateHorizontalPosition()
		{
			float direction = Mathf.Sign(_targetX - _currentX);
			float desiredSpeed = direction * _maxHorizontalSpeed;
			_velocityX = Mathf.MoveTowards(_velocityX, desiredSpeed, _acceleration * Time.deltaTime);
			_currentX += _velocityX * Time.deltaTime;

			var pos = transform.localPosition;
			pos.x = _currentX;
			transform.localPosition = pos;
		}

		private void UpdateRotation()
		{
			_rotator.UpdateRotation(_velocityX, _maxHorizontalSpeed);
		}

		private void CheckForNewTarget()
		{
			if (Mathf.Abs(_targetX - _currentX) < 0.05f)
				_targetX = GetNextOffset(_targetX);
		}

		private float GetNextOffset(float previous)
		{
			float next;
			do next = Random.Range(-_maxHorizontalOffset, _maxHorizontalOffset);
			while (Mathf.Abs(next - previous) < _minOffsetDelta);
			return next;
		}
	}
}