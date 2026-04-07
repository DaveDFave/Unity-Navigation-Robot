using UnityEngine;
using UnityEngine.InputSystem;

public class RobotSensors : MonoBehaviour
{
    public float frontSensorDistance;
    public float rightSensorDistance;
    public float leftSensorDistance;

    void Update()
    {
        RaycastHit frontHit, leftHit, rightHit;

        bool frontBlocked = Physics.Raycast(transform.position, transform.forward, out frontHit, frontSensorDistance);
        bool leftBlocked = Physics.Raycast(transform.position, -transform.right, out leftHit, leftSensorDistance);
        bool rightBlocked = Physics.Raycast(transform.position, transform.right, out rightHit, rightSensorDistance);

        // Debug rays (always visible now)
        Debug.DrawRay(transform.position, transform.forward * frontSensorDistance, Color.red);
        Debug.DrawRay(transform.position, transform.right * rightSensorDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * leftSensorDistance, Color.green);

        float leftStrength = 0f;
        float rightStrength = 0f;

        // FRONT (both motors)
        if (frontBlocked && frontHit.collider.CompareTag("Wall"))
        {
            float strength = 1 - (frontHit.distance / frontSensorDistance);
            strength = Mathf.Clamp01(strength);
            strength = strength * strength * strength;

            leftStrength = strength;
            rightStrength = strength;

            Debug.Log("Front");
        }
        else
        {
            // LEFT (only if no front)
            if (leftBlocked && leftHit.collider.CompareTag("Wall"))
            {
                leftStrength = 1 - (leftHit.distance / leftSensorDistance);
                leftStrength = Mathf.Clamp01(leftStrength);
                leftStrength = leftStrength * leftStrength * leftStrength;

                float pulse = Mathf.PingPong(Time.time * 3f, 1f);
                leftStrength *= pulse;

                Debug.Log("Left");
            }

            // RIGHT (only if no front)
            if (rightBlocked && rightHit.collider.CompareTag("Wall"))
            {
                rightStrength = 1 - (rightHit.distance / rightSensorDistance);
                rightStrength = Mathf.Clamp01(rightStrength);
                rightStrength = rightStrength * rightStrength * rightStrength;

                Debug.Log("Right");
            }
        }

        // APPLY VIBRATION (WITH CONTROLLER CHECK AT END)
        Gamepad pad = Gamepad.current;
        if (pad != null)
        {
            pad.SetMotorSpeeds(leftStrength, rightStrength);
        }
    }
}