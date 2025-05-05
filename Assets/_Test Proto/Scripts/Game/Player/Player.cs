using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class Player : MonoBehaviour
	{
		[Inject] private PlayerTurret _turret;
		[Inject] private PlayerCar _car;

		private bool _isInitialized;
		
		public void Initialize() => _isInitialized = true;

		public void SetWinCondition()
		{
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