using TestProto.Players;
using UnityEngine;
using Zenject;
using System;

namespace TestProto
{
	public class GroundCreator : MonoBehaviour
	{
		[Range(0f, 1f)] [SerializeField] private float _chunkSwitchThresholdNormalized = 0.25f;

		[Inject] private IReadOnlyPlayerCar _car;
		[Inject] private GroundChunksPool _pool;

		private GroundChunk _previousChunk;
		private GroundChunk _currentChunk;

		private int _currentChunksCount;
		private int _maxChunksCount;

		private float _nextUpdateTime;

		private bool _isCreatingGround = true;
		private bool _isFinalChunk;
		
		private const float UPDATE_INTERVAL = 0.25f;

		public event Action OnFinishCreatingGround;
		public event Action OnLastChunkCreated;

		public void Initialize(int maxChunks)
		{
			_maxChunksCount = maxChunks;
			_currentChunk = _pool.Get();
			_currentChunk.transform.position = Vector3.zero;
			_currentChunksCount++;
		}

		private bool ShouldSpawnNextChunk()
		{
			var switchZ = Mathf.Lerp(
				_currentChunk.StartPoint.position.z,
				_currentChunk.EndPoint.position.z,
				_chunkSwitchThresholdNormalized);

			return _car.Position.z > switchZ;
		}

		private void SpawnNextChunk()
		{
			var newChunk = _pool.Get();
			newChunk.transform.position += _currentChunk.EndPoint.position - newChunk.StartPoint.position;

			_currentChunksCount++;

			if (_previousChunk != null)
				_previousChunk.ReturnToPool();

			if (_currentChunksCount == _maxChunksCount)
			{
				_isFinalChunk = true;
				OnLastChunkCreated?.Invoke();
			}

			_previousChunk = _currentChunk;
			_currentChunk = newChunk;
		}
		
		private void Update()
		{
			if (_isCreatingGround && Time.timeSinceLevelLoad > _nextUpdateTime)
			{
				if (_isFinalChunk)
				{
					if (ShouldSpawnNextChunk())
					{
						_previousChunk.ReturnToPool();
						_isCreatingGround = false;
						OnFinishCreatingGround?.Invoke();
					}
				}
				else if (ShouldSpawnNextChunk())
				{
					SpawnNextChunk();	
				}
				
				_nextUpdateTime = Time.timeSinceLevelLoad + UPDATE_INTERVAL;
			}
		}
	}
}