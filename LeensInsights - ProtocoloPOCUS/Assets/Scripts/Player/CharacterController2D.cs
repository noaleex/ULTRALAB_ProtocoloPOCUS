using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;

    Vector2 motionVector;

    PlayerAction controls;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        controls = new PlayerAction();

        controls.Player.Move.performed += ctx => motionVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => motionVector = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rigidbody2d.linearVelocity = motionVector * speed;
    }
}