namespace TestProto.Enemies
{
	public class EnemiesPool : ObjectPool<Enemy>
	{
		protected override void InitializeObject(Enemy enemy)
		{
			enemy.OnDead += ReturnToPool;
		}

		protected override void CleanupObject(Enemy enemy)
		{
			enemy.OnDead -= ReturnToPool;
		}
		
		public override Enemy Get()
		{
			var enemy = base.Get();
			ResetEnemy(enemy);
			return enemy;
		}

		private void ResetEnemy(Enemy enemy) => enemy.Initialize();
	}
}