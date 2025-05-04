using UnityEngine;
using System;

namespace TestProto.Players
{
	[Serializable]
	public class CarMover
	{
		[SerializeField] private float _forwardSpeed = 5f;
		[SerializeField] private float _horizontalMoveSpeed = 1f;
		[SerializeField] private float _maxHorizontalOffset = 4f;
		[SerializeField] private float _minHorizontalOffsetDelta = 0.5f;

		private float _currentX;
		private float _targetX;

		public void Move(Transform transform)
		{
			transform.Translate(Vector3.forward * _forwardSpeed * Time.deltaTime, Space.Self);
			_currentX = Mathf.MoveTowards(_currentX, _targetX, _horizontalMoveSpeed * Time.deltaTime);
			var localPos = transform.localPosition;
			localPos.x = _currentX;
			transform.localPosition = localPos;

			if (Mathf.Abs(_currentX - _targetX) < 0.1f)
				_targetX = GetNextOffset(_targetX);
		}

		private float GetNextOffset(float previous)
		{
			float next;
			do next = UnityEngine.Random.Range(-_maxHorizontalOffset, _maxHorizontalOffset);
			while (Mathf.Abs(next - previous) < _minHorizontalOffsetDelta);
			return next;
		}
	}
}