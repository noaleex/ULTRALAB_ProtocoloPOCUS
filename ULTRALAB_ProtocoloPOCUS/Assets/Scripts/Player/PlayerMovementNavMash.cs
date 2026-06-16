using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.EventSystems;


public class PlayerMovementNavMash : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;
    private PlayerAction controls;

    [SerializeField] private Animator animator;


    public EventReference FootstepSound;
    private EventInstance footstepInstance;


    private bool isWalking = false;

    private Quaternion initialRotation;

    private bool clickPressed;



    void Awake()
    {

        controls = new PlayerAction();


        controls.UI.Click.performed += ctx =>
        {
            clickPressed = true;
        };

    }




    void OnEnable()
    {
        if(controls != null)
            controls.Enable();
    }




    void OnDisable()
    {

        if(controls != null)
            controls.Disable();


        StopFootstepSound();

    }





    void OnDestroy()
    {

        StopFootstepSound();


        if(footstepInstance.isValid())
        {
            footstepInstance.release();
        }


        if(controls != null)
        {
            controls.Dispose();
        }

    }







    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        mainCamera = Camera.main;



        // NavMesh 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.angularSpeed = 0;



        initialRotation = transform.rotation;

    }







    void Update()
    {

        if(clickPressed)
        {
            clickPressed = false;

            MoveToCursor();
        }



        // impede X -90 do Bake
        transform.rotation = initialRotation;



        CheckPath();


        UpdateAnimation();


        UpdateFootstepSound();

    }








    private void CheckPath()
    {

        if(!agent.hasPath)
            return;



        if(agent.pathStatus != NavMeshPathStatus.PathComplete)
        {

            agent.ResetPath();

            return;

        }



        if(agent.remainingDistance <= agent.stoppingDistance)
        {

            agent.ResetPath();

        }

    }









    private void MoveToCursor()
    {


        // Bloqueia clique em UI
        if(EventSystem.current != null)
        {

            if(EventSystem.current.IsPointerOverGameObject())
                return;


            if(Touchscreen.current != null)
            {

                int id =
                Touchscreen.current.primaryTouch.touchId.ReadValue();



                if(EventSystem.current.IsPointerOverGameObject(id))
                    return;

            }

        }







        Vector2 pointerPosition;



        if(Touchscreen.current != null &&
           Touchscreen.current.primaryTouch.press.isPressed)
        {

            pointerPosition =
            Touchscreen.current.primaryTouch.position.ReadValue();

        }
        else
        {

            pointerPosition =
            Mouse.current.position.ReadValue();

        }







        Vector3 target =
        mainCamera.ScreenToWorldPoint(
            new Vector3(
                pointerPosition.x,
                pointerPosition.y,
                -mainCamera.transform.position.z
            )
        );



        target.z = transform.position.z;







        NavMeshPath path = new NavMeshPath();




        if(!agent.CalculatePath(target,path))
        {

            agent.ResetPath();

            return;

        }





        if(path.status != NavMeshPathStatus.PathComplete)
        {

            agent.ResetPath();

            return;

        }





        agent.SetDestination(target);

    }









    private void UpdateAnimation()
    {

        if(agent.velocity.sqrMagnitude > 0.01f)
        {

            Vector2 dir =
            agent.velocity.normalized;



            animator.SetFloat(
                "horizontal",
                dir.x
            );


            animator.SetFloat(
                "vertical",
                dir.y
            );

        }

    }










    private void UpdateFootstepSound()
    {

        bool shouldWalk =
        agent.velocity.sqrMagnitude > 0.01f;



        if(shouldWalk && !isWalking)
        {

            PlayFootstepSound();

        }


        else if(!shouldWalk && isWalking)
        {

            StopFootstepSound();

        }

    }









    private void PlayFootstepSound()
    {

        if(!FootstepSound.IsNull)
        {

            footstepInstance =
            RuntimeManager.CreateInstance(FootstepSound);



            footstepInstance.set3DAttributes(
                RuntimeUtils.To3DAttributes(gameObject)
            );


            footstepInstance.start();



            isWalking = true;

        }

    }










    private void StopFootstepSound()
    {

        if(footstepInstance.isValid())
        {

            footstepInstance.stop(
                FMOD.Studio.STOP_MODE.IMMEDIATE
            );


            isWalking = false;

        }

    }


}