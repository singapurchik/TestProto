using UnityEngine.SceneManagement;
using System.Collections;
using TestProto.Enemies;
using TestProto.Players;
using TestProto.UI;
using UnityEngine;
using Zenject;

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

		private const float LOSE_SCREEN_DELAY = 1f;
		private const float WIN_SCREEN_DELAY = 3f;

		private enum GameState
		{
			Start = 0,
			Tutorial = 1,
			Gameplay = 2,
			GameOver = 3
		}
		
		private GameState _currentState = GameState.Start;

		private bool _isChangingState;

		private void OnEnable()
		{
			_groundCreator.OnLastChunkCreated += OnLastChunkCreated;
			_groundCreator.OnFinishCreatingGround += OnPlayerWin;
			_player.OnDead += OnPlayerLose;
		}

		private void OnDisable()
		{
			_groundCreator.OnLastChunkCreated -= OnLastChunkCreated;
			_groundCreator.OnFinishCreatingGround -= OnPlayerWin;
			_player.OnDead -= OnPlayerLose;
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

		private void OnPlayerWin()
		{
			_enemiesSpawner.DespawnAllAlive();
			_player.SetWinCondition();
			_camerasSwitcher.SwitchToWinCamera();
			StartCoroutine(ShowWinScreen());
		}

		private void OnPlayerLose()
		{
			_enemiesSpawner.DespawnAllAlive();
			StartCoroutine(ShowLoseScreen());
		}
		
		private IEnumerator ShowLoseScreen()
		{
			yield return new WaitForSeconds(LOSE_SCREEN_DELAY);
			_uiScreensSwitcher.ShowLoseScreen();
			_currentState = GameState.GameOver;
		}
		
		private IEnumerator ShowWinScreen()
		{
			yield return new WaitForSeconds(WIN_SCREEN_DELAY);
			_uiScreensSwitcher.ShowWinScreen();
			_currentState = GameState.GameOver;
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
			if (!_isChangingState)
			{
				if (_currentState == GameState.Start)
				{
					if (Input.GetMouseButtonUp(0))
						StartCoroutine(ShowTutorial());
				}
				else if (_currentState == GameState.Tutorial)
				{
					if (_playerInput.IsHorizontalInputProcess)
						StartGame();
				}
				else if (_currentState == GameState.GameOver)
				{
					if (Input.GetMouseButtonUp(0))
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				}
			}
		}
	}
}