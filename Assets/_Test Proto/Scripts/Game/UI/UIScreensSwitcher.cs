using UnityEngine;

namespace TestProto.UI
{
	public class UIScreensSwitcher : MonoBehaviour
	{
		[SerializeField] private UIScreen _startScreen;
		[SerializeField] private UIScreen _tutorialScreen;

		public void ShowStartScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Show();
		}

		public void ShowTutorialScreen()
		{
			_tutorialScreen.Show();
			_startScreen.Hide();
		}

		public void ShowGameScreen()
		{
			_tutorialScreen.Hide();
			_startScreen.Hide();
		}
	}
}