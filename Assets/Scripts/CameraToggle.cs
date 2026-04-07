using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraToggle : MonoBehaviour
{
    public CinemachineCamera topDownCam;
    public CinemachineCamera angledCam;
    public Camera mainCamera;
    public GameObject uiCanvas;
    public AudioSource audioSource;
    public AudioClip blindModeSound;
    private bool isBlindMode = false;

    private bool isTopDown = true;

    public void ToggleCamera()
    {
        if (isTopDown)
        {
            topDownCam.Priority = 0;
            angledCam.Priority = 10;
        }
        else
        {
            topDownCam.Priority = 10;
            angledCam.Priority = 0;
        }

        isTopDown = !isTopDown;
    }

    public void ToggleBlindMode()
    {
        isBlindMode = !isBlindMode;
        audioSource.PlayOneShot(blindModeSound);
        if (isBlindMode)
        {
            mainCamera.enabled = false; // turn off visuals
            uiCanvas.SetActive(false);  // hide UI
            Debug.Log("Blind Mode ON");
        }
        else
        {
            mainCamera.enabled = true;
            uiCanvas.SetActive(true);
            Debug.Log("Blind Mode OFF");
        }
    }
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            ToggleCamera();
        }
        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            ToggleBlindMode();
        }
    }
}