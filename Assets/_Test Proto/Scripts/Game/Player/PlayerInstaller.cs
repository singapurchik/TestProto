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
		[SerializeField] private Health _health;
		[SerializeField] private PlayerCar _car;
		[SerializeField] private Player _player;
		[SerializeField] private Renderer[] _renderers;
		
		public override void InstallBindings()
		{
			BindFromInstance();
			BindIntoPlayer();
			BindInstance();
		}

		private void BindFromInstance()
		{
			Container.Bind<IReadOnlyPlayerInput>().FromInstance(_input).AsSingle();
			Container.Bind<IReadOnlyHealth>().FromInstance(_health).AsSingle();
		}

		private void BindInstance()
		{
			Container.BindInstance(_visualEffects).AsSingle();
			Container.BindInstance(_renderers).AsSingle();
		}

		private void BindIntoPlayer()
		{
			Container.BindInstance(_health).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_turret).WhenInjectedIntoInstance(_player);
			Container.BindInstance(_car).WhenInjectedIntoInstance(_player);
		}
		
#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_visualEffects = GetComponentInChildren<PlayerVisualEffects>(true);
			_turret = GetComponentInChildren<PlayerTurret>(true);
			_input = GetComponentInChildren<PlayerInput>(true);
			_health = GetComponentInChildren<Health>(true);
			_player = GetComponentInChildren<Player>(true);
			_car = GetComponentInChildren<PlayerCar>(true);
		}
#endif
	}
}