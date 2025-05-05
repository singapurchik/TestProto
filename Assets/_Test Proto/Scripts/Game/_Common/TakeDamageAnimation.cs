using DG.Tweening;
using UnityEngine;

namespace TestProto.Enemies
{
	public class TakeDamageAnim
	{
		private readonly MaterialPropertyBlock _block;
		private readonly Renderer[] _renderers;
		private readonly Color _baseColor;
		private Tween _currentTween;

		private readonly int _colorID;
		private static readonly string[] _colorProps = { "_BaseColor", "_Color" };

		public TakeDamageAnim(Renderer renderer)
			: this(new[] { renderer }) { }

		public TakeDamageAnim(Renderer[] renderers)
		{
			_renderers = renderers;
			_block = new MaterialPropertyBlock();

			_colorID = ResolveColorPropertyID(_renderers[0]);
			_baseColor = _renderers[0].sharedMaterial.GetColor(_colorID);

			ApplyColor(_baseColor);
		}

		public void Play(Color flashColor, float duration)
		{
			_currentTween?.Kill();

			_currentTween = DOTween.Sequence()
				.Append(DOVirtual.Color(_baseColor, flashColor, duration, ApplyColor))
				.Append(DOVirtual.Color(flashColor, _baseColor, duration, ApplyColor));
		}

		private void ApplyColor(Color color)
		{
			_block.SetColor(_colorID, color);
			foreach (var r in _renderers)
				r.SetPropertyBlock(_block);
		}

		private int ResolveColorPropertyID(Renderer reference)
		{
			var material = reference.sharedMaterial;

			foreach (var prop in _colorProps)
				if (material.HasProperty(prop))
					return Shader.PropertyToID(prop);

			return Shader.PropertyToID("_Color");
		}
	}
}