using Cinemachine;
using UnityEngine;
using VInspector;
using Zenject;

namespace TestProto
{
	public class CamerasInstaller : MonoInstaller
	{
		[SerializeField] private CinemachineVirtualCamera _followCamera;
		[SerializeField] private CinemachineVirtualCamera _startCamera;
		[SerializeField] private CinemachineVirtualCamera _winCamera;
		[SerializeField] private CinemachineBrain _brain;

		public override void InstallBindings()
		{
			Container.BindInstance(_followCamera).WithId(CameraType.FollowCamera).AsCached();
			Container.BindInstance(_startCamera).WithId(CameraType.StartCamera).AsCached();
			Container.BindInstance(_winCamera).WithId(CameraType.WinCamera).AsCached();
			Container.BindInstance(_brain).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_brain = FindObjectOfType<CinemachineBrain>(true);
		}
#endif
	}
}