using UnityEngine;

namespace TestProto.Players
{
	public class PlayerCar : MonoBehaviour
	{
		[SerializeField] private Transform _car;
		[SerializeField] private CarMover _mover;

		public void Move() => _mover.Move(_car);
	}
}