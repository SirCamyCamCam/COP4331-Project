using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed;
    public Vector2 panLimit;
    public float scrollSpeed = 20f;

    public double minZ = -10.0;
    public double maxZ = -0.3;
     
    void Update()
    {
        Vector3 pos = transform.position;

        //If the player wants to move the camera faster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            panSpeed = 4f;
        }
        else 
        {
            panSpeed = 1f;
        }

        if (Input.GetKey("w"))
        {
            pos.y -= -panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y += -panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x -= -panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x += -panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        if (pos.z >= -70 && pos.z <= -10)
        {
            pos.z -= -scroll * scrollSpeed * 20f * Time.deltaTime;
        }
        if (pos.z > -10)
        {
            pos.z = -10;
        }
        if (pos.z < -70)
        {
            pos.z = -70;
        }
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
//z must be greater than 70 and less than -10