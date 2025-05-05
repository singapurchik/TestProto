using System.Collections;
using TestProto.Players;
using TestProto.UI;
using UnityEngine;
using Zenject;
using System;
using TestProto.Enemies;

namespace TestProto
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private int _roadLength = 5;
		
		[Inject] private UIScreensSwitcher _uiScreensSwitcher;
		[Inject] private CamerasSwitcher _camerasSwitcher;
		[Inject] private EnemiesSpawner _enemiesSpawner;
		[Inject] private GroundCreator _groundCreator;
		[Inject] private PlayerInput _playerInput;
		[Inject] private Player _player;

		private GameState _currentState = GameState.Start;

		private bool _isChangingState;

		private void OnEnable()
		{
			_groundCreator.OnFinishCreatingGround += OnFinishCreatingGround;
			_groundCreator.OnLastChunkCreated += OnLastChunkCreated;
		}

		private void OnDisable()
		{
			_groundCreator.OnFinishCreatingGround -= OnFinishCreatingGround;
			_groundCreator.OnLastChunkCreated -= OnLastChunkCreated;
		}

		private void Start()
		{
			_uiScreensSwitcher.ShowStartScreen();
			_groundCreator.Initialize(_roadLength);
			_enemiesSpawner.Enable();
		}

		private void OnLastChunkCreated()
		{
			_enemiesSpawner.Disable();
		}

		private void OnFinishCreatingGround()
		{
			_enemiesSpawner.DespawnAllAlive();
			_player.SetWinCondition();
		}
		
		private enum GameState
		{
			Start = 0,
			Tutorial = 1,
			Gameplay = 2
		}

		private IEnumerator ShowTutorial()
		{
			_isChangingState = true;
			_camerasSwitcher.SwitchToFollowCamera();
			_uiScreensSwitcher.ShowTutorialScreen();
			yield return null;
			yield return new WaitUntil(() => !_camerasSwitcher.IsCameraBlending);
			_currentState = GameState.Tutorial;
			_isChangingState = false;
		}

		private void StartGame()
		{
			_currentState = GameState.Gameplay;
			_uiScreensSwitcher.ShowGameScreen();
			_player.Initialize();
		}
		
		private void Update()
		{
			switch (_currentState)
			{
				case GameState.Start:
					if (!_isChangingState && Input.GetMouseButtonUp(0))
						StartCoroutine(ShowTutorial());
					break;
				case GameState.Tutorial:
					if (_playerInput.IsHorizontalInputProcess)
						StartGame();
					break;
				case GameState.Gameplay:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}