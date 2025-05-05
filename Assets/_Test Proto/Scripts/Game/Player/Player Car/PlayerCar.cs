using UnityEngine;

namespace TestProto.Players
{
	public class PlayerCar : MonoBehaviour, IReadOnlyPlayerCar
	{
		[SerializeField] private CarMover _mover;

		private bool _isMoveRequested;
		
		public Vector3 Position => transform.position;

		public void RequestMove() => _isMoveRequested = true;

		private void Update()
		{
			if (_isMoveRequested)
			{
				_mover.Move(transform);
				_isMoveRequested = false;
			}
		}
	}
}