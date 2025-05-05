using UnityEngine;
using Zenject;
using System;

namespace TestProto.Players
{
	public class Player : MonoBehaviour
	{
		[Inject] private PlayerTurret _turret;
		[Inject] private PlayerCar _car;

		private bool _isInitialized;

		public event Action OnDead;

		private void OnEnable() => _car.OnDestroyed += OnCarDestroyed;
		
		private void OnDisable() => _car.OnDestroyed -= OnCarDestroyed;

		public void Initialize() => _isInitialized = true;
		
		public void SetWinCondition()
		{
			_isInitialized = false;
		}
		
		private void OnCarDestroyed()
		{
			_turret.HideLaser();
			_isInitialized = false;
			OnDead?.Invoke();
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