using UnityEngine;

namespace TestProto.Enemies
{
	public class EnemyMover : MonoBehaviour
	{
		[SerializeField] private float _speedChangeRate = 2f;
		[SerializeField] private float _moveSpeed = 6f;
		
		private Vector3 _requestedTargetPosition;

		private float _currentSpeed;
		
		private bool _isMoveToTargetRequested;
		
		public float MoveSpeedNormalized => Mathf.InverseLerp(0f, _moveSpeed, _currentSpeed);

		public void RequestMoveTo(Vector3 targetPosition)
		{
			_requestedTargetPosition = targetPosition;
			_isMoveToTargetRequested = true;
		}

		private void IncreaseSpeed()
		{
			_currentSpeed = Mathf.MoveTowards(
				_currentSpeed, _moveSpeed, _speedChangeRate * Time.deltaTime);
		}
		
		private void DecreaseSpeed()
		{
			_currentSpeed = Mathf.MoveTowards(
				_currentSpeed, 0, _speedChangeRate * Time.deltaTime);
		}

		private void MoveToTarget()
		{
			transform.position = Vector3.MoveTowards(
				transform.position, _requestedTargetPosition, _currentSpeed * Time.deltaTime);
		}

		private void Update()
		{
			if (_isMoveToTargetRequested)
			{
				if (!Mathf.Approximately(_currentSpeed, _moveSpeed))
					IncreaseSpeed();

				MoveToTarget();
				_isMoveToTargetRequested = false;
			}
			else if (_currentSpeed > 0)
			{
				DecreaseSpeed();
			}
		}
	}
}