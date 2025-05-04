using UnityEngine;

namespace TestProto.Players
{
	public class PlayerCar : MonoBehaviour, IReadOnlyPlayerCar
	{
		[SerializeField] private Transform _car;
		[SerializeField] private CarMover _mover;

		public Vector3 CarPosition => _car.position;
		
		public void Move() => _mover.Move(_car);
	}
}