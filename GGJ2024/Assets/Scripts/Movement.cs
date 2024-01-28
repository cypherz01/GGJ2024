using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 rotateVector = Vector2.zero;
    private bool flyOn = false;
    private bool barrelMode = false;
    Rigidbody rigidbody;
    public float speed;
    public float flightSpeed;
    Vector3 EulerAngleVelocity;
    Vector3 EulerAngleVelocityVert;
    Vector3 NegativeEulerAngleVelocity;
    Vector3 NegativeEulerAngleVelocityVert;
    Vector3 BarrelEulerAngleVelocity;
    Vector3 NegativeBarrelEulerAngleVelocity;
    float timer = 100000f;
    private Recorder recorder;
    int ringGoal;
    int ringCount;
    public bool allRing;
    public bool canMove;
    GameObject cage;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        allRing = false;
        cage = GameObject.Find("box3");
        ringGoal = 8; ringCount = 0;   
        rigidbody = gameObject.GetComponent<Rigidbody>();
        EulerAngleVelocity = new Vector3(0, 100, 0);
        EulerAngleVelocityVert = new Vector3(100, 0, 0);
        BarrelEulerAngleVelocity = new Vector3(0, 0, 100);
        EulerAngleVelocityVert = new Vector3(100, 0, 0);
        NegativeEulerAngleVelocity = new Vector3(0, -100, 0);
        NegativeEulerAngleVelocityVert = new Vector3(-100, 0, 0);
        NegativeBarrelEulerAngleVelocity = new Vector3(0, 0, -100);
    }

    public void addRing()
    {
        
        ringCount++;
        Debug.Log(ringCount);
        if (ringCount == ringGoal)
        {
            allRing = true;
            Destroy(cage);
        }
    }

    private void Awake()
    {
        input = new CustomInput();
        recorder = GetComponent<Recorder>();
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
        input.Player.CanBarrel.performed += OnBarrelPerformed;
        input.Player.CanBarrel.canceled += OnBarrelCancelled;
    }

    private void LateUpdate()
    {
        replayData data = new replayData(this.transform.position,this.transform.rotation);
        recorder.RecordReplayFrame(data);
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
        input.Player.CanBarrel.performed -= OnBarrelPerformed;
        input.Player.CanBarrel.canceled -= OnBarrelCancelled;
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

    private void OnBarrelPerformed(InputAction.CallbackContext context)
    {
        barrelMode = context.ReadValueAsButton();
    }

    private void OnBarrelCancelled(InputAction.CallbackContext context)
    {
        barrelMode = false;
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (moveVector.x > 0)
            {

                rigidbody.MovePosition((rigidbody.rotation * Vector3.right * Time.fixedDeltaTime * speed) + rigidbody.position);
            }

            if (moveVector.x < 0)
            {
                rigidbody.MovePosition((rigidbody.rotation * Vector3.left * Time.fixedDeltaTime * speed) + rigidbody.position);
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

            if ((rotateVector.x > 0) && (barrelMode))
            {

                Quaternion deltaRotation = Quaternion.Euler(BarrelEulerAngleVelocity * Time.fixedDeltaTime);

                rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
            }

            if ((rotateVector.x < 0) && (barrelMode))
            {
                Quaternion deltaRotation = Quaternion.Euler(NegativeBarrelEulerAngleVelocity * Time.fixedDeltaTime);

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

                rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
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

}
