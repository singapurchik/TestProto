using TestProto.Players.Turrets;
using UnityEngine;
using Zenject;

namespace TestProto.Players
{
	public class Player : MonoBehaviour
	{
		[Inject] private Turret _turret;
		
		private void Awake()
		{
			_turret.Enable();
		}
	}
}