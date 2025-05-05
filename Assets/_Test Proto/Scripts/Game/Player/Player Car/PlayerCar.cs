using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	[RequireComponent(typeof(BoxCollider))]
	public class PlayerCar : MonoBehaviour, IReadOnlyPlayerCar
	{
		[SerializeField] private GameObject _wheelsTrails;
		
		[Inject] private PlayerCarAnimator _animator;
		[Inject] private IReadOnlyHealth _health;
		[Inject] private PlayerCarMover _mover;

		private BoxCollider _collider;

		private bool _isMoveRequested;

		public Vector3 Position => transform.position;
		
		private void Awake() => _collider = GetComponent<BoxCollider>();
		
		private void OnEnable() => _health.OnTakeDamage += OnTakeDamage;

		private void OnDisable() => _health.OnTakeDamage -= OnTakeDamage;

		public void DisableWheelsTrails() => _wheelsTrails.SetActive(false);
		
		public void DisableCollider() => _collider.enabled = false;
		
		public void RequestMove() => _isMoveRequested = true;
		
		private void OnTakeDamage() => _animator.PlayTakeDamageAnim();

		private void Update()
		{
			if (_isMoveRequested)
			{
				_mover.Move();
				_isMoveRequested = false;
			}
		}
	}
}