using UnityEngine;
using Zenject;

namespace TestProto.Enemies
{
	public class Enemy : MonoBehaviour
	{
		[Inject] private EnemyStateMachine _stateMachine;

		private void Start()
		{
			_stateMachine.Initialize();
		}
	}
}