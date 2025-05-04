using TestProto.Projectiles;
using TestProto.Players;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto
{
	public class GameSceneInstaller : MonoInstaller
	{
		[SerializeField] private ProjectilePool _projectilePool;

		public override void InstallBindings()
		{
			Container.BindInstance(_projectilePool).WhenInjectedInto<TurretAttacker>();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_projectilePool = FindObjectOfType<ProjectilePool>(true);
		}
#endif
	}
}