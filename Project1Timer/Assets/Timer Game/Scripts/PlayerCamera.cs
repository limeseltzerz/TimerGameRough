using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX; // x and y sensitivities 
    public float sensY;

    public Transform orientation;

    float xRotation; // x and y rotation of the camera
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // the cursor is locked in the middle of the screen and invisible for first player movement
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp so that you can't move the camera vertically more than 90 degrees!
        // transform.rotation and orientation.rotation rotate cam and orientation, pretty self explanatory.
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
