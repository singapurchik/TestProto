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

		public override void InstallBindings()
		{
			Container.BindInstance(_carAnimator).WhenInjectedIntoInstance(_car);
			Container.BindInstance(_mover).WhenInjectedIntoInstance(_car);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_carAnimator = GetComponentInChildren<PlayerCarAnimator>(true);
			_mover = GetComponentInChildren<PlayerCarMover>(true);
			_car = GetComponentInChildren<PlayerCar>(true);
		}
#endif
	}
}