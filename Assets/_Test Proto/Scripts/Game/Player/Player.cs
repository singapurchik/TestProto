using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class Player : MonoBehaviour
	{
		[Inject] private PlayerTurret _turret;
		[Inject] private PlayerCar _car;

		private void Update()
		{
			_turret.RequestAttack();
			_turret.RequestRotate();
			_car.RequestMove();
		}
	}
}