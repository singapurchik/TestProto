using TestProto.Players;
using UnityEngine;
using Zenject;

namespace TestProto
{
	public class GroundCreator : MonoBehaviour
	{
		[Range(0f, 1f)] [SerializeField] private float _chunkSwitchThresholdNormalized = 0.25f;

		[Inject] private IReadOnlyPlayerCar _car;
		[Inject] private GroundChunksPool _pool;
		
		private GroundChunk _currentChunk;
		private GroundChunk _lastChunk;

		private void Start()
		{
			_currentChunk = _pool.Get();
			_currentChunk.transform.position = Vector3.zero;
		}

		private void Update()
		{
			var spawnChunkPosition = Mathf.Lerp(
				_currentChunk.StartPoint.position.z,
				_currentChunk.EndPoint.position.z,
				_chunkSwitchThresholdNormalized);
			
			if (_car.CarPosition.z > spawnChunkPosition)
			{
				var newChunk = _pool.Get();
				newChunk.transform.position += _currentChunk.EndPoint.position - newChunk.StartPoint.position;
				
				if (_lastChunk != null)
					_lastChunk.ReturnToPool();
				
				_lastChunk = _currentChunk;
				_currentChunk = newChunk;
			}
		}
	}
}