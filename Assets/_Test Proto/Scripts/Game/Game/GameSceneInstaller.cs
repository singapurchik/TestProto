using TestProto.Projectiles;
using TestProto.Enemies;
using TestProto.Players;
using TestProto.UI;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto
{
	public class GameSceneInstaller : MonoInstaller
	{
		[SerializeField] private UIScreensSwitcher _uiScreensSwitcher;
		[SerializeField] private GroundChunksPool _groundChunksPool;
		[SerializeField] private CamerasSwitcher _camerasSwitcher;
		[SerializeField] private ProjectilePool _projectilePool;
		[SerializeField] private EnemiesSpawner _enemiesSpawner;
		[SerializeField] private GroundCreator _groundCreator;
		[SerializeField] private EnemiesPool _enemiesPool;
		[SerializeField] private PlayerInput _playerInput;
		[SerializeField] private PlayerCar _playerCar;
		[SerializeField] private Player _player;
		[SerializeField] private Game _game;

		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyPlayerCar>().FromInstance(_playerCar).AsSingle();
			BindPools();
			BindGame();
		}

		private void BindPools()
		{
			Container.BindInstance(_groundChunksPool).WhenInjectedIntoInstance(_groundCreator);
			Container.BindInstance(_enemiesPool).WhenInjectedIntoInstance(_enemiesSpawner);
			Container.BindInstance(_projectilePool).WhenInjectedInto<TurretAttacker>();
		}
		private void BindGame()
		{
			Container.BindInstance(_uiScreensSwitcher).WhenInjectedIntoInstance(_game);
			Container.BindInstance(_camerasSwitcher).WhenInjectedIntoInstance(_game);
			Container.BindInstance(_enemiesSpawner).WhenInjectedIntoInstance(_game);
			Container.BindInstance(_groundCreator).WhenInjectedIntoInstance(_game);
			Container.BindInstance(_playerInput).WhenInjectedIntoInstance(_game);
			Container.BindInstance(_player).WhenInjectedIntoInstance(_game);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_uiScreensSwitcher = FindObjectOfType<UIScreensSwitcher>(true);
			_groundChunksPool = FindObjectOfType<GroundChunksPool>(true);
			_camerasSwitcher = FindObjectOfType<CamerasSwitcher>(true);
			_projectilePool = FindObjectOfType<ProjectilePool>(true);
			_enemiesSpawner = FindObjectOfType<EnemiesSpawner>(true);
			_groundCreator = FindObjectOfType<GroundCreator>(true);
			_enemiesPool = FindObjectOfType<EnemiesPool>(true);
			_playerInput = FindObjectOfType<PlayerInput>(true);
			_playerCar = FindObjectOfType<PlayerCar>(true);
			_player = FindObjectOfType<Player>(true);
			_game = FindObjectOfType<Game>(true);
		}
#endif
	}
}