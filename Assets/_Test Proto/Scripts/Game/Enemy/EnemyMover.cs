using UnityEngine;

namespace TestProto.Enemies
{
	public class EnemyMover : MonoBehaviour
	{
		[SerializeField] private float _speedChangeRate = 2f;
		[SerializeField] private float _walkSpeed = 2f;
		[SerializeField] private float _runSpeed = 6f;
		
		private Vector3 _requestedTargetPosition;

		private float _requestedSpeed;
		private float _currentSpeed;
		
		private bool _isMoveToTargetRequested;

		private const float IDLE_SPEED = 0f;
		
		public void RequestRunTo(Vector3 targetPosition) => RequestMoveTo(targetPosition, _runSpeed);
		
		public void WalkRunTo(Vector3 targetPosition) => RequestMoveTo(targetPosition, _walkSpeed);

		private void RequestMoveTo(Vector3 targetPosition, float targetSpeed)
		{
			_requestedTargetPosition = targetPosition;
			_requestedSpeed = targetSpeed;
			_isMoveToTargetRequested = true;
		}

		private void Update()
		{
			if (_isMoveToTargetRequested)
			{
				if (!Mathf.Approximately(_currentSpeed, _requestedSpeed))
					_currentSpeed = Mathf.MoveTowards(
						_currentSpeed, _requestedSpeed, _speedChangeRate * Time.deltaTime);
				
				transform.position = Vector3.MoveTowards(
					transform.position, _requestedTargetPosition, _currentSpeed * Time.deltaTime);
				
				_isMoveToTargetRequested = false;
			}
			else if (_currentSpeed > 0)
			{
				_currentSpeed = Mathf.MoveTowards(_currentSpeed, 0, _speedChangeRate * Time.deltaTime);
			}
		}
	}
}