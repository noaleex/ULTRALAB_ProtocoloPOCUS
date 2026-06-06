using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD.Studio;

public class PlayerMovementNavMash : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;
    private PlayerAction controls;

    [SerializeField] private Animator animator;
    
    public EventReference FootstepSound;
    private EventInstance footstepInstance;
    private bool isWalking = false;

    void Awake()
    {
        controls = new PlayerAction();

        controls.UI.Click.performed += ctx => MoveToCursor();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
        StopFootstepSound();
    }
    
    void OnDestroy()
    {
        StopFootstepSound();
        if (footstepInstance.isValid())
        {
            footstepInstance.release();
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }

    void Update()
    {
        UpdateAnimation();
        UpdateFootstepSound();
    }

    private void UpdateAnimation()
    {
        if (agent.velocity.sqrMagnitude > 0.01f) 
        {
            Vector2 moveDir = agent.velocity.normalized;
            
            animator.SetFloat("horizontal", moveDir.x);
            animator.SetFloat("vertical", moveDir.y);
        }
    }
    
    private void UpdateFootstepSound()
    {
        bool shouldWalk = agent.velocity.sqrMagnitude > 0.01f;
        
        if (shouldWalk && !isWalking)
        {
            PlayFootstepSound();
        }
        else if (!shouldWalk && isWalking)
        {
            StopFootstepSound();
        }
    }
    
    private void PlayFootstepSound()
    {
        if (!FootstepSound.IsNull)
        {
            footstepInstance = RuntimeManager.CreateInstance(FootstepSound);
            footstepInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            footstepInstance.start();
            isWalking = true;
        }
    }
    
    private void StopFootstepSound()
    {
        if (footstepInstance.isValid())
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            isWalking = false;
        }
    }

    private void MoveToCursor()
    {
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        
        Vector3 targetPath = mainCamera.ScreenToWorldPoint(pointerPosition);
        targetPath.z = 0;

        agent.SetDestination(targetPath);
    }
}