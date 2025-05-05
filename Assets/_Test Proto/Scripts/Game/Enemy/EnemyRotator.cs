using UnityEngine;

namespace TestProto.Enemies
{
	public class EnemyRotator : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed = 720f;
		[SerializeField] private float _angleThreshold = 1f;

		private Quaternion _requestedTargetRotation;
		
		private bool _isRotateToRequested;

		public bool IsLookingAtTarget { get; private set; }

		public void RequestRotateTo(Vector3 target)
		{
			target -= transform.position;
			target.y = 0f;
			_requestedTargetRotation = Quaternion.LookRotation(target);
			_isRotateToRequested = true;
		}

		private void RotateToTarget()
		{
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				_requestedTargetRotation,
				_rotationSpeed * Time.deltaTime);
		}

		private void Update()
		{
			if (_isRotateToRequested)
			{
				RotateToTarget();
				IsLookingAtTarget = Quaternion.Angle(transform.rotation, _requestedTargetRotation) < _angleThreshold;
				_isRotateToRequested = false;
			}
			else
			{
				IsLookingAtTarget = false;
			}
		}
	}
}