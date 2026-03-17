using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotSensors : MonoBehaviour
{
    public float sensorDistance;

    public bool frontBlocked;
    public bool leftBlocked;
    public bool rightBlocked;

    // Track if vibration is already active for each sensor
    private bool vibratingFront = false;
    private bool vibratingLeft = false;
    private bool vibratingRight = false;
    private bool vibratingDeadEnd = false;

    void Update()
    {
        Gamepad pad = Gamepad.current;
        if (pad == null)
        {
           return; 
        } 

        RaycastHit frontHit;
        RaycastHit leftHit;
        RaycastHit rightHit;

        // FRONT SENSOR
        frontBlocked = Physics.Raycast(transform.position, transform.forward, out frontHit, sensorDistance);

        // LEFT SENSOR
        leftBlocked = Physics.Raycast(transform.position, -transform.right, out leftHit, sensorDistance);

        // RIGHT SENSOR
        rightBlocked = Physics.Raycast(transform.position, transform.right, out rightHit, sensorDistance);

        // Debug lines
        Debug.DrawRay(transform.position, transform.forward * sensorDistance, Color.red);
        Debug.DrawRay(transform.position, transform.right * sensorDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * sensorDistance, Color.green);

        bool deadEnd = frontBlocked && leftBlocked && rightBlocked &&
                       frontHit.collider.CompareTag("Wall") &&
                       leftHit.collider.CompareTag("Wall") &&
                       rightHit.collider.CompareTag("Wall");


        // Dead end
        if(deadEnd)
        {
            if (!vibratingDeadEnd)
            {
                Vibrate(1f, 1f, -1f);
                vibratingDeadEnd = true;
                vibratingFront = vibratingLeft = vibratingRight = false;
            }
        }
        else
        {
            vibratingDeadEnd = false;
            // Wall detection
            if (frontBlocked && frontHit.collider.CompareTag("Wall"))
            {
                if (!vibratingFront)
                    {
                        Vibrate(1f, 1f, 0.5f);
                        vibratingFront = true;
                    }
            }
            else
            {
                vibratingFront = false;
            }
            if (leftBlocked && leftHit.collider.CompareTag("Wall"))
            {
                if (!vibratingLeft)
                    {
                        Vibrate(0.5f, 0.5f, 0.1f);
                        vibratingLeft = true;
                    }
            }
            else
            {
                vibratingLeft = false;
            }
            if (rightBlocked && rightHit.collider.CompareTag("Wall"))
            {
                if (!vibratingRight)
                    {
                        Vibrate(0.5f, 0.5f, 0.1f);
                        vibratingRight = true;
                    }
            }
            else
            {
                vibratingRight = false;
            }
        }
    }

    public void Vibrate(float lowFreq, float highFreq, float duration)
    {
        Gamepad pad = Gamepad.current;
        if(pad != null)
        {
            pad.SetMotorSpeeds(lowFreq, highFreq);
            if (duration > 0)
            {
                Invoke("StopVibrate", duration);
            }
        }
    }

    public void StopVibrate()
    {
        Gamepad pad = Gamepad.current;
        if(pad != null)
        {
            pad.SetMotorSpeeds(0,0);
        }
    }
}