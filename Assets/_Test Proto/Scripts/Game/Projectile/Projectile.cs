using System.Collections;
using UnityEngine;
using System;

namespace TestProto.Projectiles
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private ProjectileTrigger _trigger;
		[SerializeField] private TrailRenderer _moveTrail;
		[SerializeField] private Transform _body;
		[SerializeField] private float _moveSpeed = 15f;
		[SerializeField] private float _maxDistance = 20f;

		private Coroutine _currentEnableTrailRoutine;
		
		private Vector3 _currentDirection;

		private float _timeToStopMove;
		private float _currentDamage;

		private bool _isDeathKickLaunched;
		private bool _isMoving;

		private const float MAX_LIFE_TIME = 2f;

		public event Action<Projectile> OnMoveComplete;

		private void OnDisable() => _trigger.OnTriggerTarget -= OnTriggerTarget;
		
		private void OnEnable() => _trigger.OnTriggerTarget += OnTriggerTarget;

		public void Initialize() => gameObject.SetActive(false);

		private void OnTriggerTarget(IDamageableCollider damageableCollider)
		{
			damageableCollider.TakeDamage(_currentDamage);
			StopMove();
		}

		private void StopMove()
		{
			if (_currentEnableTrailRoutine != null)
				StopCoroutine(_currentEnableTrailRoutine);
			
			_isMoving = false;
			_moveTrail.enabled = false;
			OnMoveComplete?.Invoke(this);
		}

		public void Launch(Vector3 targetPosition, float damage)
		{
			_currentEnableTrailRoutine = StartCoroutine(EnableTrailRoutine());
			_timeToStopMove = Time.timeSinceLevelLoad + MAX_LIFE_TIME;
			_body.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			_body.LookAt(targetPosition);
			_currentDirection = (targetPosition - _body.transform.position).normalized;
			_currentDamage = damage;
			_isMoving = true;
		}

		private IEnumerator EnableTrailRoutine()
		{
			yield return null;
			_moveTrail.enabled = true;
		}

		private void Update()
		{
			if (_isMoving)
			{
				_body.Translate(_currentDirection * (_moveSpeed * Time.deltaTime), Space.World);
				
				if (Time.timeSinceLevelLoad > _timeToStopMove)
					StopMove();
			}
		}
	}

}