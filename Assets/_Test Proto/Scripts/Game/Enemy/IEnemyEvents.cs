using System;

namespace TestProto.Enemies
{
	public interface IEnemyEvents
	{
		public event Action<Enemy> OnDead;

		public void InvokeOnDead();
	}
}