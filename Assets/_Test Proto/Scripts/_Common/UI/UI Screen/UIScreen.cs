using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine;
using VInspector;

namespace TestProto.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class UIScreen : MonoBehaviour
	{
		private struct AnimatedObject
		{
			public readonly RectTransform RectTransform;
			public readonly Vector3 MaxScale;

			public AnimatedObject(RectTransform rectTransform, Vector3 currentScale)
			{
				RectTransform = rectTransform;
				MaxScale = currentScale;
			}
		}

		[SerializeField] private bool _isFullyDisabled = true;
		[ShowIf(nameof(_isFullyDisabled))]
		[SerializeField] private List<GameObject> _children = new();
		[EndIf]
		[SerializeField] private bool _isAnimated;
		[SerializeField] private bool _isHasFadeEffect;
		[ShowIf(nameof(_isHasFadeEffect))]
		[SerializeField] private float _showFadeDuration = 0.2f;
		[SerializeField] private float _hideFadeDuration;
		[EndIf]
		[ShowIf(nameof(_isAnimated))]
		[SerializeField] private RectTransform[] _objectsAnimatedOnShow;
		[SerializeField] private Vector3 _showAnimScaleTarget = new (0.05f, 0.05f, 0.05f);
		[SerializeField] private float _showAnimDuration = 0.5f;
		[SerializeField] private int _vibrato;
		[SerializeField] private int _elasticity;
		[SerializeField] private Ease _showAnimEaseType = Ease.OutBack;
		[SerializeField] private float _spawnObjectsDelay;
		[EndIf]
		
		protected CanvasGroup CanvasGroup;

		private AnimatedObject[] _animatedObjects;
		private Coroutine _currentShowRoutine;

		private bool _isEventsShowInEditor;

		public bool IsProcessShow { get; private set; }
		public bool IsProcessHide { get; private set; }
		public bool IsShown { get; protected set; }

		[ShowIf(nameof(_isEventsShowInEditor))]
		public UnityEvent OnStartShow;
		public UnityEvent OnStartHide;
		public UnityEvent OnHidden;
		public UnityEvent OnShown;
		[EndIf]
		
		protected virtual void Awake()
		{
			CanvasGroup = GetComponent<CanvasGroup>();

			_animatedObjects = new AnimatedObject[_objectsAnimatedOnShow.Length];
			for (var i = 0; i < _animatedObjects.Length; i++)
			{
				var currentObject = _objectsAnimatedOnShow[i];
				_animatedObjects[i] = new AnimatedObject(currentObject, currentObject.localScale);
			}

			IsShown = CanvasGroup.alpha != 0;
		}

		public virtual void Hide()
		{
			if (!IsShown || IsProcessHide)
				return;

			IsProcessHide = true;
			IsProcessShow = false;

			OnStartHide?.Invoke();

			if (_isHasFadeEffect)
			{
				CanvasGroup.DOFade(0, _hideFadeDuration)
					.OnComplete(HideComplete);
			}
			else
			{
				if (_isAnimated)
				{
					if (_currentShowRoutine != null)
						StopCoroutine(_currentShowRoutine);

					foreach (var animatedObject in _animatedObjects)
					{
						animatedObject.RectTransform.DOKill();
						animatedObject.RectTransform.localScale = animatedObject.MaxScale;
					}
				}

				CanvasGroup.alpha = 0;
				HideComplete();
			}
		}

		public virtual void Show()
		{
			if (IsShown || IsProcessShow)
				return;

			IsProcessShow = true;
			IsProcessHide = false;

			OnStartShow?.Invoke();

			if (_isFullyDisabled && _children.Count > 0)
				foreach (var child in _children)
					child.gameObject.SetActive(true);

			if (_isHasFadeEffect)
			{
				CanvasGroup.DOFade(1, _showFadeDuration)
					.OnComplete(ShowComplete);
			}
			else
			{
				CanvasGroup.alpha = 1;

				if (_isAnimated)
					_currentShowRoutine = StartCoroutine(ShowRoutine());
				else
					ShowComplete();
			}
		}

		private IEnumerator ShowRoutine()
		{
			foreach (var animatedObject in _animatedObjects)
			{
				ShowAnim(animatedObject);
				yield return new WaitForSeconds(_spawnObjectsDelay);
			}

			yield return new WaitForSeconds(_showAnimDuration);
			ShowComplete();
		}

		protected virtual void ShowComplete()
		{
			IsProcessShow = false;
			IsShown = true;
			OnShown?.Invoke();
		}

		protected virtual void HideComplete()
		{
			IsProcessHide = false;
			IsShown = false;

			if (_isFullyDisabled && _children.Count > 0)
				foreach (var child in _children)
					child.gameObject.SetActive(false);

			OnHidden?.Invoke();
		}

		private void ShowAnim(AnimatedObject animatedObject)
		{
			animatedObject.RectTransform.DOPunchScale(_showAnimScaleTarget, _showAnimDuration, _vibrato, _elasticity)
				.SetEase(_showAnimEaseType);
		}

#if UNITY_EDITOR
		[ShowIf(nameof(_isFullyDisabled))]
		[Button]
		private void FindChildren()
		{
			_children.Clear();

			foreach (Transform child in transform)
				_children.Add(child.gameObject);
		}

		[EndIf]
		[Button]
		private void EventsShowInEditor() => _isEventsShowInEditor = !_isEventsShowInEditor;
#endif
	}
}