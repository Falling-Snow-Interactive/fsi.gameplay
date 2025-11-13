using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Gameplay.Sample.Gameplay.Players.StateMachines.States
{
    public class SampleControlState : SamplePlayerState
    {
	    [Header("Camera")]

	    [SerializeField]
	    private Transform cameraTransform;
	    
	    [Header("Movement")]

	    [Min(0)]
	    [SerializeField]
	    private float movementSpeed = 5f;

	    [Min(0)]
	    [SerializeField]
	    private float rotationSpeed = 10f;
	    
	    [Min(0f)]
	    [SerializeField]
	    private float accelerationSmoothTime = 0.08f;
	    
	    private Vector3 velSmoothRef;
	    private Vector2 movementInputValue;
	    
	    [Header("Input")]
	    
	    [SerializeField]
	    private InputActionReference movementInputRef;
	    private InputAction movementInput;
	    
	    [SerializeField]
	    private InputActionReference interactRef;
	    private InputAction interactAction;

	    #region MonoBehaviour
	    
	    protected virtual void Awake()
	    {
		    movementInput = movementInputRef.ToInputAction();
		    interactAction = interactRef.ToInputAction();
	    }

	    protected virtual void Start()
	    {
		    if (!cameraTransform)
		    {
			    if (Camera.main)
			    {
				    cameraTransform = Camera.main.transform;
			    }
			    else
			    {
				    Debug.LogWarning($"Camera could not be found. {gameObject.name}", gameObject);
			    }
		    }
	    }

	    protected virtual void OnEnable()
	    {
		    movementInput.Enable();
		    interactAction?.Enable();
	    }

	    protected virtual void OnDisable()
	    {
		    movementInput.Disable();
		    interactAction?.Disable();
	    }

	    protected virtual void Update()
	    {
		    if (Active)
		    {
			    movementInputValue = movementInput.ReadValue<Vector2>();
		    }
	    }
	    
	    #endregion
	    
	    #region States

	    public override void OnUpdate()
	    {
		    base.OnUpdate();
		    UpdateMovement();
	    }
        
        #endregion
        
        #region Transition Control

        public override bool CanTransitionIn() => true;
        
        public override bool CanTransitionOut() => true;
        
        #endregion
        
        #region Movement

        protected virtual void UpdateMovement()
        {
	        if (!SamplePlayer.Rigidbody)
	        {
		        return;
	        }

	        Vector2 input = Vector2.ClampMagnitude(movementInputValue, 1f);

	        Vector3 camForward = cameraTransform.transform.forward;
	        Vector3 camRight = cameraTransform.transform.right;
	        
	        camForward.y = 0f;
	        camRight.y = 0f;
	        camForward.Normalize();
	        camRight.Normalize();

	        Vector3 targetDir = camForward * input.y + camRight * input.x;
	        Vector3 targetVelocity = targetDir.normalized * (movementSpeed * input.magnitude);

	        Vector3 currentXZ = new(SamplePlayer.Rigidbody.linearVelocity.x, 0f, SamplePlayer.Rigidbody.linearVelocity.z);
	        Vector3 targetXZ = new(targetVelocity.x, 0f, targetVelocity.z);
	        Vector3 newXZ = Vector3.SmoothDamp(
	                                           currentXZ,
	                                           targetXZ,
	                                           ref velSmoothRef,
	                                           accelerationSmoothTime,
	                                           Mathf.Infinity,
	                                           Time.fixedDeltaTime);
            
	        SamplePlayer.Rigidbody.linearVelocity = new Vector3(newXZ.x, SamplePlayer.Rigidbody.linearVelocity.y, newXZ.z);

	        Vector3 lookDir = newXZ;
	        if (lookDir.sqrMagnitude > 0.0001f)
	        {
		        Quaternion targetRot = Quaternion.LookRotation(lookDir.normalized, Vector3.up);
		        SamplePlayer.Rigidbody.MoveRotation(Quaternion.Slerp(SamplePlayer.Rigidbody.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
	        }
            
	        SamplePlayer.Visuals?.SetMovement(SamplePlayer.Rigidbody.linearVelocity, false);
        }
	    
        #endregion
    }
}
