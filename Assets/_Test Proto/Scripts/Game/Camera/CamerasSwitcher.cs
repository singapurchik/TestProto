using Cinemachine;
using UnityEngine;
using Zenject;

namespace TestProto
{
	public class CamerasSwitcher : MonoBehaviour
	{
		[Inject (Id  = CameraType.FollowCamera)] private CinemachineVirtualCamera _followCamera;
		[Inject (Id  = CameraType.StartCamera)] private CinemachineVirtualCamera _startCamera;
		[Inject (Id  = CameraType.WinCamera)] private CinemachineVirtualCamera _winCamera;

		[Inject] private CinemachineBrain _brain;

		public bool IsCameraBlending => _brain.IsBlending;
		
		public void SwitchToFollowCamera()
		{
			_followCamera.enabled = true;
			_startCamera.enabled = false;
			_winCamera.enabled = false;
		}
		
		public void SwitchToWinCamera()
		{
			_followCamera.enabled = false;
			_startCamera.enabled = false;
			_winCamera.enabled = true;
		}
	}
}