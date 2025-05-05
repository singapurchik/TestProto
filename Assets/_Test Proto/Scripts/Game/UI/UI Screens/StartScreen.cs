using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TestProto.UI
{
	public class StartScreen : UIScreen
	{
		[SerializeField] private TextMeshProUGUI _tapToStartText;
		[SerializeField] private float _pulseScale = 1.1f;
		[SerializeField] private float _pulseDuration = 0.6f;
		[SerializeField] private float _fadeMinAlpha = 0.3f;

		private Tween _scaleTween;
		private Tween _fadeTween;
		private Vector3 _initialScale;
		private Color _initialColor;

		protected override void Awake()
		{
			base.Awake();
			_initialScale = _tapToStartText.transform.localScale;
			_initialColor = _tapToStartText.color;
		}

		public override void Show()
		{
			base.Show();
			StartPulseAnimation();
		}

		private void StartPulseAnimation()
		{
			_scaleTween = _tapToStartText.transform
				.DOScale(_initialScale * _pulseScale, _pulseDuration)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutQuad)
				.SetLink(_tapToStartText.gameObject);

			_fadeTween = _tapToStartText
				.DOColor(new Color(_initialColor.r, _initialColor.g, _initialColor.b, _fadeMinAlpha), _pulseDuration)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutQuad)
				.SetLink(_tapToStartText.gameObject);
		}

		private void StopPulseAnimation()
		{
			_scaleTween?.Kill();
			_fadeTween?.Kill();

			_tapToStartText.transform.localScale = _initialScale;
			_tapToStartText.color = _initialColor;
		}

		protected override void HideComplete()
		{
			StopPulseAnimation();
			base.HideComplete();
		}
	}
}