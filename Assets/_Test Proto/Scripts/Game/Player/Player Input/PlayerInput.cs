using UnityEngine;

namespace TestProto.Players
{
	public class PlayerInput : MonoBehaviour, IReadOnlyPlayerInput
	{
		public float HorizontalInput { get; private set; }

		public bool IsActive { get; private set; } = true;

		private Vector2 _lastPointerPosition;

		public void Disable() => IsActive = false;
		
		public void Enable() => IsActive = true;

		private void Update()
		{
			if (!IsActive)
			{
				HorizontalInput = 0f;
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				_lastPointerPosition = Input.mousePosition;
			}

			if (Input.GetMouseButton(0))
			{
				var currentPos = (Vector2)Input.mousePosition;
				var delta = currentPos - _lastPointerPosition;
				HorizontalInput = delta.x;
				_lastPointerPosition = currentPos;
			}
			else
			{
				HorizontalInput = 0f;
			}
		}
	}
}