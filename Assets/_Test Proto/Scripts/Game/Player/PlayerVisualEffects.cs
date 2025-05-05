using UnityEngine;

namespace TestProto.Players
{
	public class PlayerVisualEffects : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _carDestroyEffect;

		public void PlayCarDestroyEffect() => _carDestroyEffect.Play();
	}
}