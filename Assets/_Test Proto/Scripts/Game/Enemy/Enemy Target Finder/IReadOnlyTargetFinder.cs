using TestProto.Players;
using System;

namespace TestProto.Enemies
{
	public interface IReadOnlyTargetFinder
	{
		public IReadOnlyPlayerCar CurrentTarget { get;}
		
		public bool IsHasTarget {get;}
		
		public event Action OnTargetFound;
	}
}