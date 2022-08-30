using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class MovementController : MonoBehaviour
{
    public float speed = 1f;
    public float sprintMultiplier = 2f;
    private bool sprinting = false;
    public float turnSpeed = 1f;
    public XRNode inputDevice;
    public XRNode inputDeviceTurn;

    public Camera camera;
    private Vector2 inputAxis;
    private Vector2 inputAxisTurn;
    InputDevice device;
    InputDevice deviceTurn;

    public PlayerHealth playerHealthComponent;
    public Rigidbody playerRigidbody;
    public CharacterController playerController;

    private Vector3 moveVector;

    private void OnCollisionEnter(Collision collision)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputDevice);
        deviceTurn = InputDevices.GetDeviceAtXRNode(inputDeviceTurn);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        deviceTurn.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisTurn);

        bool sprintButtonPressed = false;
        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out sprintButtonPressed);

        Quaternion headYaw = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        //Reset move vector
        moveVector = Vector3.zero;

        //Sprinting switch
        if (sprintButtonPressed)
            sprinting = true;

        if (inputAxis.normalized.magnitude < 0.7)
            sprinting = false;

        //Check if character is grounded
        if (playerController.isGrounded == false)
        {
            //Add our gravity Vecotr
            moveVector += Physics.gravity;
        }

        if (playerController.isGrounded)
        {
            moveVector.y = 0f;
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
        }

        if (sprinting)
        {
            //playerRigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed * sprintMultiplier);
            //playerRigidbody.AddForce(direction * Time.fixedDeltaTime * speed * sprintMultiplier);
            moveVector += direction * speed * sprintMultiplier;
        }
        else
        {
            //playerRigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
            //playerRigidbody.AddForce(direction * Time.fixedDeltaTime * speed);
            moveVector += direction * speed;
        }

        playerController.Move(moveVector * Time.fixedDeltaTime);

        //playerController.height = camera.transform.position.y;

        transform.Rotate(new Vector3(0f, inputAxisTurn.x * Time.fixedDeltaTime * turnSpeed, 0f));
    }

    private void FixedUpdate() 
    {
        
    }
}
