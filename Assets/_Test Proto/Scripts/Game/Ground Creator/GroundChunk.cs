using UnityEngine;
using System;

namespace TestProto
{
	public class GroundChunk : MonoBehaviour
	{
		[field: SerializeField] public Transform StartPoint { get; private set; }
		[field: SerializeField] public Transform EndPoint { get; private set; }

		public event Action<GroundChunk> OnReadyToReturn;
		public void Initialize() => gameObject.SetActive(false);

		public void ReturnToPool() => OnReadyToReturn?.Invoke(this);
	}
}