using DG.Tweening;
using UnityEngine;

namespace TestProto.Players
{
	public class PlayerTurretAnimator : MonoBehaviour
	{
		[SerializeField] private Transform _body;
		[SerializeField] private float _fireScaleY = 0.9f;
		[SerializeField] private float _duration = 0.1f;
		[SerializeField] private Ease _ease = Ease.OutQuad;

		private Tween _currentTween;
		private Vector3 _originalScale;

		private void Awake()
		{
			_originalScale = _body.localScale;
		}

		public void PlayAttackAim()
		{
			_currentTween?.Kill();
			_body.localScale = _originalScale;

			var scaled = _originalScale;
			scaled.y *= _fireScaleY;

			_currentTween = _body.DOScale(scaled, _duration)
				.SetEase(_ease)
				.OnComplete(() =>
				{
					_currentTween = _body.DOScale(_originalScale, _duration)
						.SetEase(Ease.InQuad);
				});
		}
	}
}