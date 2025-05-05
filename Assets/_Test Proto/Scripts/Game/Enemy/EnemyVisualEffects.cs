using UnityEngine;

namespace TestProto.Enemies
{
	public class EnemyVisualEffects : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _takeDamageEffect;
		[SerializeField] private ParticleSystem _deathEffect;

		private void Awake()
		{
			_takeDamageEffect.transform.SetParent(transform.parent);
			_deathEffect.transform.SetParent(transform.parent);
		}

		public void PlayTakeDamageEffect()
		{
			_takeDamageEffect.transform.position = transform.position + Vector3.up;
			_takeDamageEffect.Play();
		}
		
		public void PlayDeathEffect()
		{
			_deathEffect.transform.position = transform.position + Vector3.up;
			_deathEffect.Stop();
			_deathEffect.Play();
		}
	}
}