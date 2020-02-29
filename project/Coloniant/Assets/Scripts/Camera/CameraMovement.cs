using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed ;
    
    public Vector2 panLimit;
    public float scrollSpeed = 20f;

    public double minZ = -10.0;
    public double maxZ = -0.3;
     
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            panSpeed = 2f;
        }
        else {
            panSpeed = 1f;
        }
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z -= scroll * scrollSpeed * 20f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}