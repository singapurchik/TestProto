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

		private TakeDamageAnim _takeDamageAnim;

		private const int TAKE_DAMAGE_LAYER_INDEX = 1;

		private readonly int _takeDamageAnimHash = Animator.StringToHash("Take Damage");
		private readonly int _locomotion = Animator.StringToHash("locomotionValue");

		private void Awake() => _takeDamageAnim = new TakeDamageAnim(_renderer);

		public void PlayTakeDamageAnim()
		{
			_takeDamageAnim.Play(_takeDamageColor, _takeDamageAnimDuration);
			_animator.Play(_takeDamageAnimHash, TAKE_DAMAGE_LAYER_INDEX, 0);
		}

		public void UpdateLocomotionAnim(float value) => _animator.SetFloat(_locomotion, value);
	}
}