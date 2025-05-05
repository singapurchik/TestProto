using System.Collections.Generic;
using TestProto.Players;
using UnityEngine;
using Zenject;

namespace TestProto.Enemies
{
    public class EnemiesSpawner : MonoBehaviour
    {
	    [SerializeField] private float _spawnZOffsetMin = 15f;
	    [SerializeField] private float _spawnZOffsetMax = 40f;
	    [SerializeField] private Vector2 _xRange = new (-8f, 8f);
	    [SerializeField] private int _maxAlive = 10;
	    [SerializeField] private float _minSpacing = 1f;
	    [SerializeField] private float _checkInterval = 0.25f;

        [Inject] private IReadOnlyPlayerCar _car;
        [Inject] private EnemiesPool _pool;

        private readonly List<Enemy> _alive = new();

        private float _nextCheckTime;

        private bool _isActive;

        private const float DESPAWN_OFFSET = 5f;
        
        public void Disable() => _isActive = false;
        
        public void Enable() => _isActive = true;

        public void DespawnAllAlive()
        {
	        foreach (var enemy in _alive.ToArray())
		        enemy.Kill();
        }

        private void TryDespawn()
        {
            for (int i = _alive.Count - 1; i >= 0; i--)
	            if (_alive[i].transform.position.z < _car.Position.z - DESPAWN_OFFSET)
		            _alive[i].Kill();
        }

        private void TrySpawn()
        {
	        const int maxAttempts = 10;
	        int attempts = 0;

	        while (_alive.Count < _maxAlive && attempts < maxAttempts)
	        {
		        var candidatePos = GetRandomAheadPosition();
		        attempts++;

		        if (IsFarEnoughFromOthers(candidatePos))
		        {
			        var enemy = _pool.Get();
			        enemy.transform.position = candidatePos;
			        enemy.OnDead += RemoveFromList;
			        _alive.Add(enemy);
		        }
	        }
        }


        private Vector3 GetRandomAheadPosition()
        {
            var z = Random.Range(_spawnZOffsetMin, _spawnZOffsetMax) + _car.Position.z;
            var x = Random.Range(_xRange.x, _xRange.y);
            return new Vector3(x, 0f, z);
        }

        private bool IsFarEnoughFromOthers(Vector3 pos)
        {
            foreach (var enemy in _alive)
                if (Vector3.SqrMagnitude(enemy.transform.position - pos) < _minSpacing * _minSpacing)
                    return false;
            
            return true;
        }

        private void RemoveFromList(Enemy enemy)
        {
	        enemy.OnDead -= RemoveFromList;
	        _alive.Remove(enemy);
        }
        
        private void Update()
        {
	        if (_isActive && Time.timeSinceLevelLoad > _nextCheckTime)
	        {
		        TryDespawn();
		        TrySpawn();
		        
				_nextCheckTime = Time.timeSinceLevelLoad + _checkInterval;
	        }
        }
    }
}
