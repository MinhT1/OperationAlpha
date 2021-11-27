using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;

    float verticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Input of Mouse Position
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        //Vertical rotation
        verticalRotation -= y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //horizontal rotation
        player.Rotate(Vector3.up * x);
    }
}
