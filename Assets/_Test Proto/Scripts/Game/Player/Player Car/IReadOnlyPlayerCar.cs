using UnityEngine;

namespace TestProto.Players
{
	public interface IReadOnlyPlayerCar
	{
		public Vector3 Position { get; }
	}
}