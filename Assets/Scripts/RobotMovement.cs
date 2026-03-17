using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RobotMovement: MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public float turnSpeed;
    private Vector3 moveDir;
    public InputActionReference move;

    private void Update()
    {
        moveDir = move.action.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {   //Movement
        Vector3 forwardMovement = transform.forward * moveDir.z * moveSpeed;
        rb.linearVelocity = new Vector3(forwardMovement.x, 0, forwardMovement.z);

        //Rotation
        float turnAmount = moveDir.x * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));
    }
}
