using Fsi.StateMachines;
using UnityEngine;

namespace Fsi.Gameplay.Sample.Gameplay.Players.StateMachines.States
{
	public abstract class SamplePlayerState : MonoState
	{
		[Header("Player")]
		
		[SerializeField]
		private SamplePlayerController samplePlayer;
		protected SamplePlayerController SamplePlayer => samplePlayer;
	}
}