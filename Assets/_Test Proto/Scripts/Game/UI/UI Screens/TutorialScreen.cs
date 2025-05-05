using DG.Tweening;
using UnityEngine;

namespace TestProto.UI
{
	public class TutorialScreen : UIScreen
	{
		[SerializeField] private RectTransform _hand;
		[SerializeField] private RectTransform _handLeftPoint;
		[SerializeField] private RectTransform _handRightPoint;
		[SerializeField] private float _moveDuration = 0.8f;
		[SerializeField] private Ease _ease = Ease.InOutSine;

		private Tween _handMoveTween;

		public override void Show()
		{
			PlayHandMoveAnim();
			base.Show();
		}

		private void PlayHandMoveAnim()
		{
			_hand.anchoredPosition = _handLeftPoint.anchoredPosition;

			_handMoveTween = _hand
				.DOAnchorPos(_handRightPoint.anchoredPosition, _moveDuration)
				.SetEase(_ease)
				.SetLoops(-1, LoopType.Yoyo)
				.SetLink(_hand.gameObject);
		}

		private void StopHandMoveAnim()
		{
			_handMoveTween?.Kill();
		}

		protected override void HideComplete()
		{
			StopHandMoveAnim();
			base.HideComplete();
		}
	}
}