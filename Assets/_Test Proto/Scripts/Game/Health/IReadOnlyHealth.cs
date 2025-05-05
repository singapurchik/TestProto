using System;

namespace TestProto
{
	public interface IReadOnlyHealth
	{
		public float CurrentHealth { get; }

		public bool IsEmpty { get; }
		public bool IsFull { get; }

		public event Action OnFullHealthTakeDamage;
		public event Action OnZeroHealth;
		public event Action OnTakeDamage;
		public event Action OnTakeHeal;
	}
}