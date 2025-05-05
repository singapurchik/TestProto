using System.Collections;
using TestProto.Players;
using TestProto.UI;
using UnityEngine;
using Zenject;
using System;

namespace TestProto
{
	public class Game : MonoBehaviour
	{
		[Inject] private UIScreensSwitcher _uiScreensSwitcher;
		[Inject] private CamerasSwitcher _camerasSwitcher;
		[Inject] private PlayerInput _playerInput;
		[Inject] private Player _player;

		private GameState _currentState = GameState.Start;

		private bool _isChangingState;

		private void Start()
		{
			_uiScreensSwitcher.ShowStartScreen();
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