using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerVisualEffects _visualEffects;
		[SerializeField] private PlayerTurret _turret;
		[SerializeField] private PlayerInput _input;
		[SerializeField] private PlayerCar _car;
		[SerializeField] private Player _player;
		
		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyPlayerInput>().FromInstance(_input).AsSingle();
			Container.BindInstance(_turret).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_car).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_visualEffects).AsSingle();
		}
		
#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_visualEffects = GetComponentInChildren<PlayerVisualEffects>(true);
			_turret = GetComponentInChildren<PlayerTurret>(true);
			_input = GetComponentInChildren<PlayerInput>(true);
			_player = GetComponentInChildren<Player>(true);
			_car = GetComponentInChildren<PlayerCar>(true);
		}
#endif
	}
}