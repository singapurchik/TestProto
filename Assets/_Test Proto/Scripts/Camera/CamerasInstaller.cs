using Cinemachine;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto
{
	public class CamerasInstaller : MonoInstaller
	{
		[SerializeField] private CamerasSwitcher _cameraSwitcher;
		[SerializeField] private CinemachineVirtualCamera _startCamera;
		[SerializeField] private CinemachineVirtualCamera _followCamera;
		[SerializeField] private CinemachineBrain _brain;

		public override void InstallBindings()
		{
			Container.BindInstance(_followCamera).WithId(CameraType.FollowCamera).AsCached();
			Container.BindInstance(_startCamera).WithId(CameraType.StartCamera).AsCached();
			Container.BindInstance(_brain).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_cameraSwitcher = FindObjectOfType<CamerasSwitcher>(true);
			_brain = FindObjectOfType<CinemachineBrain>(true);
		}
#endif
	}
}