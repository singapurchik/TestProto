using TestProto.Projectiles;
using TestProto.Enemies;
using TestProto.Players;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto
{
	public class GameSceneInstaller : MonoInstaller
	{
		[SerializeField] private GroundChunksPool _groundChunksPool;
		[SerializeField] private ProjectilePool _projectilePool;
		[SerializeField] private EnemiesSpawner _enemiesSpawner;
		[SerializeField] private GroundCreator _groundCreator;
		[SerializeField] private EnemiesPool _enemiesPool;
		[SerializeField] private PlayerCar _playerCar;

		public override void InstallBindings()
		{
			Container.BindInstance(_groundChunksPool).WhenInjectedIntoInstance(_groundCreator);
			Container.BindInstance(_enemiesPool).WhenInjectedIntoInstance(_enemiesSpawner);
			Container.BindInstance(_projectilePool).WhenInjectedInto<TurretAttacker>();
			Container.Bind<IReadOnlyPlayerCar>().FromInstance(_playerCar).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_groundChunksPool = FindObjectOfType<GroundChunksPool>(true);
			_projectilePool = FindObjectOfType<ProjectilePool>(true);
			_enemiesSpawner = FindObjectOfType<EnemiesSpawner>(true);
			_groundCreator = FindObjectOfType<GroundCreator>(true);
			_enemiesPool = FindObjectOfType<EnemiesPool>(true);
			_playerCar = FindObjectOfType<PlayerCar>(true);
		}
#endif
	}
}