using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 rotateVector = Vector2.zero;
    private bool flyOn = false;
    Rigidbody rigidbody;
    public float speed;
    public float flightSpeed;
    Vector3 EulerAngleVelocity;
    Vector3 EulerAngleVelocityVert;
    Vector3 NegativeEulerAngleVelocity;
    Vector3 NegativeEulerAngleVelocityVert;
    float timer = 100000f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        EulerAngleVelocity = new Vector3(0, 100, 0);
        EulerAngleVelocityVert = new Vector3(100, 0, 0);
        NegativeEulerAngleVelocity = new Vector3(0, -100, 0);
        NegativeEulerAngleVelocityVert = new Vector3(-100, 0, 0);
    }

    private void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Rotate.performed += OnRotatePerformed;
        input.Player.Rotate.canceled += OnRotateCancelled;
        input.Player.Fly.performed += OnFlyPerformed;
        input.Player.Fly.canceled += OnFlyCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
        input.Player.Rotate.performed -= OnRotatePerformed;
        input.Player.Rotate.canceled -= OnRotateCancelled;
        input.Player.Fly.performed -= OnFlyPerformed;
        input.Player.Fly.canceled -= OnFlyCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        rotateVector = context.ReadValue<Vector2>();
    }

    private void OnRotateCancelled(InputAction.CallbackContext context)
    {
        rotateVector = Vector2.zero;
    }

    private void OnFlyPerformed(InputAction.CallbackContext context)
    {
        flyOn = context.ReadValueAsButton();
    }

    private void OnFlyCancelled(InputAction.CallbackContext context)
    {
        flyOn = false;
    }





    // Update is called once per frame
    void FixedUpdate()
    {

        if (moveVector.x > 0)
        {

            rigidbody.MovePosition((rigidbody.rotation*Vector3.right*Time.fixedDeltaTime*speed) + rigidbody.position);
        }

        if (moveVector.x < 0)
        {
            rigidbody.MovePosition((rigidbody.rotation * Vector3.left * Time.fixedDeltaTime*speed) + rigidbody.position);
        }

        if (moveVector.y > 0)
        {
            rigidbody.MovePosition((rigidbody.rotation * Vector3.forward * Time.fixedDeltaTime * speed) + rigidbody.position);
        }

        if (moveVector.y < 0)
        {
            rigidbody.MovePosition((rigidbody.rotation * Vector3.back * Time.fixedDeltaTime * speed) + rigidbody.position);
        }

        if (rotateVector.x > 0)
        {

            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Time.fixedDeltaTime);

            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        if (rotateVector.x < 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(NegativeEulerAngleVelocity * Time.fixedDeltaTime);

            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        if (rotateVector.y > 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityVert * Time.fixedDeltaTime);

            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        if (rotateVector.y < 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(NegativeEulerAngleVelocityVert * Time.fixedDeltaTime);

            rigidbody.MoveRotation(rigidbody.rotation *  deltaRotation);
        }

        if ((rotateVector.y == 0) && (rotateVector.x == 0))
        {
            rigidbody.angularVelocity = Vector3.zero;
        }

        if (flyOn)
        {
                rigidbody.velocity = Vector3.zero;
                rigidbody.MovePosition((Vector3.up * (Time.fixedDeltaTime) * flightSpeed) + rigidbody.position);
        }



    }

}
