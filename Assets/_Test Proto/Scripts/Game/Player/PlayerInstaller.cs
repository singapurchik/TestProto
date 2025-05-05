using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerInput _input;
		[SerializeField] private PlayerTurret _turret;
		[SerializeField] private Player _player;
		[SerializeField] private PlayerCar _car;
		
		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyPlayerInput>().FromInstance(_input).AsSingle();
			Container.BindInstance(_turret).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_car).WhenInjectedIntoInstance(_player);
		}
		
#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_input = GetComponentInChildren<PlayerInput>(true);
			_turret = GetComponentInChildren<PlayerTurret>(true);
			_player = GetComponentInChildren<Player>(true);
			_car = GetComponentInChildren<PlayerCar>(true);
		}
#endif
	}
}