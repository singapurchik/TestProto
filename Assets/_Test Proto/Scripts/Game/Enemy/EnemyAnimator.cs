using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TestProto.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private float _takeDamageAnimDuration = 0.1f;
        [SerializeField] private Color _takeDamageColor = Color.white;

        [Inject] private SkinnedMeshRenderer _renderer;
        [Inject] private Animator _animator;

        private MaterialPropertyBlock _block;
        private Tween _currentTakeDamageTween;
        private Color _baseColor;
        
        private int _colorID;

        private readonly int _locomotion = Animator.StringToHash("locomotionValue");

        private static readonly string[] _colorProps = { "_BaseColor", "_Color" };

        private void Awake()
        {
            _block = new MaterialPropertyBlock();

            foreach (var prop in _colorProps)
            {
                if (_renderer.sharedMaterial.HasProperty(prop))
                {
                    _colorID = Shader.PropertyToID(prop);
                    break;
                }
            }

            _renderer.GetPropertyBlock(_block);
            _baseColor = _renderer.sharedMaterial.GetColor(_colorID);
            ApplyColor(_baseColor);
        }

        public void UpdateLocomotionAnim(float value) =>
            _animator.SetFloat(_locomotion, value);

        public void PlayTakeDamageAnim()
        {
            _currentTakeDamageTween?.Kill();

            _currentTakeDamageTween = DOTween.Sequence()
                .Append(DOVirtual.Color(_baseColor, _takeDamageColor, _takeDamageAnimDuration, ApplyColor))
                .Append(DOVirtual.Color(_takeDamageColor, _baseColor, _takeDamageAnimDuration, ApplyColor));
        }

        private void ApplyColor(Color color)
        {
            _block.SetColor(_colorID, color);
            _renderer.SetPropertyBlock(_block);
        }
    }
}
