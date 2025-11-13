using Fsi.Gameplay.Gameplay;
using Fsi.Gameplay.Visuals;
using Fsi.StateMachines;
using UnityEngine;

namespace Fsi.Gameplay.Sample.Gameplay.Players
{
    public class SamplePlayerController : MonoBehaviour
    {
	    [Header("Physics")]
	    
	    [SerializeField]
	    private new Rigidbody rigidbody;
	    public Rigidbody Rigidbody => rigidbody;

	    [SerializeField]
	    private GroundCheck groundCheck;
	    public GroundCheck GroundCheck => groundCheck;
	    
	    [Header("State Machine")]
	    
	    [SerializeField]
	    private MonoState startState;
	    
	    [SerializeField]
	    private MonoState controlState;
	    
	    [SerializeReference]
	    private StateMachine stateMachine;

	    [Header("Visuals")]

	    [SerializeField]
	    private CharacterVisuals visuals;
	    public CharacterVisuals Visuals => visuals;

	    #region MonoBehaviour
	    
	    protected virtual void Awake()
	    {
		    SetupStateMachine();
	    }

	    protected virtual void Start()
	    {
		    stateMachine?.Start();
	    }

	    protected virtual void FixedUpdate()
	    {
		    stateMachine?.Update();
	    }
	    
	    #endregion

	    #region State Machine

	    protected virtual void SetupStateMachine()
	    {
		    stateMachine = new StateMachine(startState)
		                   {
			                   Debugging = true
		                   };

		    // Setup transitions
		    stateMachine.From(startState).To(controlState);
	    }

	    public void GoToState(IState state)
	    {
		    stateMachine.SetState(state);
	    }

	    #endregion
    }
}
