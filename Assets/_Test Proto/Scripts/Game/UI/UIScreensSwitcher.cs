using UnityEngine;

namespace TestProto.UI
{
	public class UIScreensSwitcher : MonoBehaviour
	{
		[SerializeField] private UIScreen _startScreen;
		[SerializeField] private UIScreen _tutorialScreen;
		[SerializeField] private UIScreen _loseScreen;
		[SerializeField] private UIScreen _winScreen;

		public void ShowStartScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Show();
			_loseScreen.Hide();
			_winScreen.Hide();
		}

		public void ShowTutorialScreen()
		{
			_tutorialScreen.Show();
			_startScreen.Hide();
			_loseScreen.Hide();
			_winScreen.Hide();
		}

		public void ShowGameScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Hide();
			_loseScreen.Hide();
			_winScreen.Hide();
		}

		public void ShowLoseScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Hide();
			_loseScreen.Show();
			_winScreen.Hide();
		}
		
		public void ShowWinScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Hide();
			_loseScreen.Hide();
			_winScreen.Show();
		}
	}
}