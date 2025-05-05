using Cinemachine;
using UnityEngine;

namespace TestProto
{
	public class CameraShaker : MonoBehaviour
	{
		[SerializeField] private CinemachineImpulseSource _takeDamageImpulse;
		[SerializeField] private CinemachineImpulseSource _destroyCarImpulse;
		
		public void PlayTakeDamageEffect() => _takeDamageImpulse.GenerateImpulse();
		
		public void PlayDestroyCarEffect() => _destroyCarImpulse.GenerateImpulse();
	}
}