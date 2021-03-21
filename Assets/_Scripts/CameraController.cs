using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;

    public float mouseSensitivity = 1000.0f;
    public Transform playerBody;
    private float XRotation = 0.0f;
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        /*Cursor.lockState= CursorLockMode.Locked;*/
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.Paused)
        {
            return;
        }

        /*float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;*/

        float mouseX = joystick.Horizontal * horizontalSensitivity;
        float mouseY = joystick.Vertical * verticalSensitivity;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
