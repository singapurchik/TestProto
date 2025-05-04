using UnityEngine;
using System;

namespace TestProto.Projectiles
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _moveTrailEffect;
		[SerializeField] private Transform _body;
		[SerializeField] private float _moveSpeed = 15f;
		[SerializeField] private float _maxDistance = 20f;

		private Vector3 _currentDirection;

		private float _timeToStopMove;
		private float _currentDamage;

		private bool _isDeathKickLaunched;
		private bool _isMoving;

		private const float MAX_LIFE_TIME = 5f;

		public event Action<Projectile> OnMoveComplete;

		public void Initialize()
		{
			gameObject.SetActive(false);
			_moveTrailEffect.transform.SetParent(transform.parent);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageableCollider damageableCollider))
			{
				damageableCollider.TakeDamage(_currentDamage);
				StopMove();
			}
		}

		private void StopMove()
		{
			_isMoving = false;
			_moveTrailEffect.Stop();
			OnMoveComplete?.Invoke(this);
		}

		public void Launch(Vector3 targetPosition, float damage)
		{
			_timeToStopMove = Time.timeSinceLevelLoad + MAX_LIFE_TIME;
			_body.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
			_body.LookAt(targetPosition);
			_currentDirection = (targetPosition - _body.transform.position).normalized;
			_moveTrailEffect.Play();
			_currentDamage = damage;
			_isMoving = true;
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