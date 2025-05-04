using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players.Turrets
{
	public class TurretInstaller : MonoInstaller
	{
		[SerializeField] private TurretAttacker _attacker;
		[SerializeField] private TurretRotator _rotator;
		[SerializeField] private Turret _turret;

		public override void InstallBindings()
		{
			Container.BindInstance(_attacker).WhenInjectedIntoInstance(_turret);
			Container.BindInstance(_rotator).WhenInjectedIntoInstance(_turret);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_attacker = GetComponentInChildren<TurretAttacker>(true);
			_rotator = GetComponentInChildren<TurretRotator>(true);
			_turret = GetComponentInChildren<Turret>(true);
		}
#endif
	}
}