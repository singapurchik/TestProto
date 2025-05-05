using TestProto.Enemies;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace TestProto.Players
{
	public class PlayerCarAnimator : MonoBehaviour
	{
		[SerializeField] private float _takeDamageAnimDuration = 0.1f;
		[SerializeField] private Color _takeDamageColor = Color.white;
		[SerializeField] private Vector3 _punchScale = new Vector3(0.15f, 0f, 0.15f); // punch по XZ

		[Inject] private Renderer[] _renderers;

		private TakeDamageAnim _takeDamageAnim;
		private Tween _currentTakeDamageTween;

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
				transform.DOPunchScale(_punchScale, _takeDamageAnimDuration, vibrato: 1, elasticity: 0.5f);
		}
	}
}