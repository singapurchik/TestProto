using TestProto.Enemies;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace TestProto.Players
{
	public class PlayerCarAnimator : MonoBehaviour
	{
		[SerializeField] private float _takeDamageAnimDuration = 0.1f;
		[SerializeField] private Color _takeDamageColor = Color.white;
		[SerializeField] private Vector3 _takeDamageScale = new (0.15f, 0f, 0.15f);

		[Inject] private Renderer[] _renderers;

		private TakeDamageAnim _takeDamageAnim;
		private Tween _currentTakeDamageTween;

		private const float TAKE_DAMAGE_ELASTICITY = 0.5f;
		private const int TAKE_DAMAGE_VIBRATO = 1;

		private void Awake()
		{
			_takeDamageAnim = new TakeDamageAnim(_renderers);
		}

		public void PlayTakeDamageAnim()
		{
			_takeDamageAnim.Play(_takeDamageColor, _takeDamageAnimDuration);

			_currentTakeDamageTween?.Kill();
			transform.localScale = Vector3.one;
			
			_currentTakeDamageTween =
				transform.DOPunchScale(
					_takeDamageScale, _takeDamageAnimDuration, TAKE_DAMAGE_VIBRATO, TAKE_DAMAGE_ELASTICITY);
		}
	}
}