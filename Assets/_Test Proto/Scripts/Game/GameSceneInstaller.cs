using TestProto.Projectiles;
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
		[SerializeField] private PlayerCar _playerCar;

		public override void InstallBindings()
		{
			Container.BindInstance(_groundChunksPool).WhenInjectedInto<GroundCreator>();
			Container.BindInstance(_projectilePool).WhenInjectedInto<TurretAttacker>();
			Container.Bind<IReadOnlyPlayerCar>().FromInstance(_playerCar).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_groundChunksPool = FindObjectOfType<GroundChunksPool>(true);
			_projectilePool = FindObjectOfType<ProjectilePool>(true);
			_playerCar = FindObjectOfType<PlayerCar>(true);
		}
#endif
	}
}