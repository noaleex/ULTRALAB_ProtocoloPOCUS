using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    PlayerAction controls;
    Animator animator;
    
    public EventReference FoodStep;
    private EventInstance footstepInstance;
    private bool isWalking = false;
    private float stopGraceTimer = 0f;
    private const float StopGraceTime = 0.2f;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        controls = new PlayerAction();

        controls.Player.Move.performed += ctx => motionVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => motionVector = Vector2.zero;

        animator = GetComponent<Animator>();
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

    void Update()
    {
        animator.SetFloat("horizontal", motionVector.x);
        animator.SetFloat("vertical", motionVector.y);
        UpdateFootstepSound();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rigidbody2d.linearVelocity = motionVector * speed;
    }
    
    private void UpdateFootstepSound()
    {
        bool shouldWalk = motionVector.sqrMagnitude > 0.01f;

        if (shouldWalk)
        {
            stopGraceTimer = StopGraceTime;
            if (!isWalking)
            {
                PlayFootstepSound();
            }
        }
        else
        {
            if (stopGraceTimer > 0f)
            {
                stopGraceTimer -= Time.deltaTime;
            }
            else if (isWalking)
            {
                StopFootstepSound();
            }
        }
    }
    
    private void PlayFootstepSound()
    {
        if (!FoodStep.IsNull)
        {
            footstepInstance = RuntimeManager.CreateInstance(FoodStep);
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
            footstepInstance.release();
            isWalking = false;
        }
    }
}