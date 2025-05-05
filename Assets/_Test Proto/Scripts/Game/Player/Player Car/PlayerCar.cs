using UnityEngine;
using Zenject;
using System;

namespace TestProto.Players
{
	[RequireComponent(typeof(BoxCollider))]
	public class PlayerCar : MonoBehaviour, IReadOnlyPlayerCar
	{
		[Inject] private PlayerVisualEffects _visualEffects;
		[Inject] private PlayerCarAnimator _animator;
		[Inject] private Renderer[] _carRenderers;
		[Inject] private PlayerCarMover _mover;
		[Inject] private Health _health;

		private BoxCollider _collider;

		private bool _isMoveRequested;
		
		public Vector3 Position => transform.position;

		public event Action OnDestroyed;

		private void Awake()
		{
			_collider = GetComponent<BoxCollider>();
		}

		private void OnEnable()
		{
			_health.OnTakeDamage += OnTakeDamage;
			_health.OnZeroHealth += OnZeroHealth;
		}

		private void OnDisable()
		{
			_health.OnTakeDamage -= OnTakeDamage;
			_health.OnZeroHealth -= OnZeroHealth;
		}

		private void OnTakeDamage()
		{
			_animator.PlayTakeDamageAnim();
		}

		private void OnZeroHealth()
		{
			_collider.enabled = false;
			
			foreach (var carRenderer in _carRenderers)
				carRenderer.enabled = false;
			
			_visualEffects.PlayCarDestroyEffect();
			OnDestroyed?.Invoke();
		}

		public void RequestMove() => _isMoveRequested = true;

		private void Update()
		{
			if (_isMoveRequested)
			{
				_mover.Move();
				_isMoveRequested = false;
			}
		}
	}
}