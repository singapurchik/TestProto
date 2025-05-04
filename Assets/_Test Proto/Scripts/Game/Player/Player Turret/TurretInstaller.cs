using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players.Turrets
{
	public class TurretInstaller : MonoInstaller
	{
		[SerializeField] private TurretRotator _rotator;
		[SerializeField] private Turret _turret;

		public override void InstallBindings()
		{
			Container.BindInstance(_rotator).WhenInjectedIntoInstance(_turret);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_rotator = GetComponentInChildren<TurretRotator>(true);
			_turret = GetComponentInChildren<Turret>(true);
		}
#endif
	}
}