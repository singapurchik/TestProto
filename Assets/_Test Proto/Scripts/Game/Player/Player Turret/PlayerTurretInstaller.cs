using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players
{
	public class PlayerTurretInstaller : MonoInstaller
	{
		[SerializeField] private PlayerTurretAttacker _attacker;
		[SerializeField] private PlayerTurretAnimator _animator;
		[SerializeField] private PlayerTurretRotator _rotator;
		[SerializeField] private PlayerTurret _turret;

		public override void InstallBindings()
		{
			Container.BindInstance(_attacker).WhenInjectedIntoInstance(_turret);
			Container.BindInstance(_rotator).WhenInjectedIntoInstance(_turret);
			Container.BindInstance(_animator).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_attacker = GetComponentInChildren<PlayerTurretAttacker>(true);
			_animator = GetComponentInChildren<PlayerTurretAnimator>(true);
			_rotator = GetComponentInChildren<PlayerTurretRotator>(true);
			_turret = GetComponentInChildren<PlayerTurret>(true);
		}
#endif
	}
}