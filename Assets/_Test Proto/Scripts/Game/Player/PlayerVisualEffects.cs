using UnityEngine;

namespace TestProto.Players
{
	public class PlayerVisualEffects : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _carDestroyEffect;
		[SerializeField] private ParticleSystem _fireworkEffect;

		public void PlayCarDestroyEffect() => _carDestroyEffect.Play();
		
		public void PlayFireworkEffect() => _fireworkEffect.Play();
	}
}