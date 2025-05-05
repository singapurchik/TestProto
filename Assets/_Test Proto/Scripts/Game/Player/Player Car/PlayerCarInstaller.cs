using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto.Players
{
	public class PlayerCarInstaller : MonoInstaller
	{
		[SerializeField] private PlayerCarAnimator _carAnimator;
		[SerializeField] private PlayerCarMover _mover;
		[SerializeField] private PlayerCar _car;
		[SerializeField] private Health _health;
		[SerializeField] private Renderer[] _animatedRenderers;

		public override void InstallBindings()
		{
			Container.BindInstance(_animatedRenderers).WhenInjectedIntoInstance(_carAnimator);
			Container.BindInstance(_carAnimator).WhenInjectedIntoInstance(_car);
			Container.BindInstance(_health).WhenInjectedIntoInstance(_car);
			Container.BindInstance(_mover).WhenInjectedIntoInstance(_car);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_carAnimator = GetComponentInChildren<PlayerCarAnimator>(true);
			_mover = GetComponentInChildren<PlayerCarMover>(true);
			_health = GetComponentInChildren<Health>(true);
			_car = GetComponentInChildren<PlayerCar>(true);
		}
#endif
	}
}