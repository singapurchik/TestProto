using TestProto.Players.Turrets;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerInput _input;
		[SerializeField] private Turret _turret;
		[SerializeField] private Player _player;
		
		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyPlayerInput>().FromInstance(_input).AsSingle();
			Container.BindInstance(_turret).WhenInjectedIntoInstance(_player);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_input = GetComponentInChildren<PlayerInput>(true);
			_turret = GetComponentInChildren<Turret>(true);
			_player = GetComponentInChildren<Player>(true);
		}
#endif
	}
}