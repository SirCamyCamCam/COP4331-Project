// --------------------------------------------------------------
// Coloniant - Camera Movement                              2/29/2020
// Author(s): Henry Alvarez
// Contact: henryalvarez5798@knights.ucf.edu
// --------------------------------------------------------------

using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //z must be greater than 70 and less than maxHeight

    #region Run-Time Fields

    public float panSpeed;
    public Vector2 panLimit;
    public float scrollSpeed = 20f;
    public float minHeight = -24;
    public float maxHeight = -10;

    #endregion

    #region Monobehaviors

    void Update()
    {
        CameraControl();
        CameraSpeed();
        CameraScroll();
    }

    #endregion

    #region Public Methods

    public bool CameraSpeed()
    {
        //If the player wants to move the camera faster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            panSpeed = 20f;
        }
        else
        {
            panSpeed = 1f;
        }

        return true;
    }

    public bool CameraControl(){
        Vector3 pos = transform.position;

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

        transform.position = pos;

        return true;
    }

    public bool CameraScroll()
    {
        Vector3 pos = transform.position;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        if (pos.z >= minHeight && pos.z <= maxHeight)
        {
            pos.z -= -scroll * scrollSpeed * 20f * Time.deltaTime;
        }
        if (pos.z > maxHeight)
        {
            pos.z = maxHeight;
        }
        if (pos.z < minHeight)
        {
            pos.z = minHeight;
        }
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;

        return true;
    }

    #endregion
}