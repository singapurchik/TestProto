using UnityEngine;
using Zenject;
using System;

namespace TestProto.Players
{
	public class Player : MonoBehaviour
	{
		[Inject] private PlayerVisualEffects _visualEffects;
		[Inject] private CameraShaker _cameraShaker;
		[Inject] private Renderer[] _renderers;
		[Inject] private PlayerTurret _turret;
		[Inject] private PlayerCar _car;
		[Inject] private Health _health;

		private bool _isInitialized;

		public event Action OnDead;

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
		
		public void Initialize() => _isInitialized = true;

		private void OnTakeDamage()
		{
			_cameraShaker.PlayTakeDamageEffect();
		}

		private void OnZeroHealth()
		{
			_visualEffects.PlayCarDestroyEffect();
			_cameraShaker.PlayDestroyCarEffect();
			_car.DisableWheelsTrails();
			_car.DisableCollider();
			_turret.HideLaser();
			
			foreach (var carRenderer in _renderers)
				carRenderer.enabled = false;
			
			_isInitialized = false;
			OnDead?.Invoke();
		}

		
		public void SetWinCondition()
		{
			_visualEffects.PlayFireworkEffect();
			_isInitialized = false;
		}
		private void Update()
		{
			if (_isInitialized)
			{
				_turret.RequestAttack();
				_turret.RequestRotate();
				_car.RequestMove();	
			}
		}
	}
}