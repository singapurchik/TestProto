using UnityEngine;

namespace TestProto.Players
{
	public class PlayerInput : MonoBehaviour, IReadOnlyPlayerInput
	{
		private Vector2 _lastPointerPosition;
		
		private const float READ_HORIZONTAL_INPUT_STEP = 0.01f;
		
		public float HorizontalInput { get; private set; }

		public bool IsHorizontalInputProcess => Mathf.Abs(HorizontalInput) > READ_HORIZONTAL_INPUT_STEP;
		public bool IsActive { get; private set; } = true;

		public void Disable() => IsActive = false;
		
		public void Enable() => IsActive = true;

		private void UpdateHorizontalInput()
		{
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

		private void Update()
		{
			if (IsActive)
			{
				UpdateHorizontalInput();
			}
			else
			{
				HorizontalInput = 0f;
			}
		}
	}
}