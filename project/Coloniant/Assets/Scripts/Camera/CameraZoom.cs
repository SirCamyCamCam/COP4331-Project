//By: Amin Kavehzadeh 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraZoom : MonoBehaviour
{
 
    private Camera camera;
    private float targetZoom;
    public float zoomFactor;
    public float zoomLerpSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        targetZoom = camera.orthographicSize;
    }
 
    // Update is called once per frame
    void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 10f, 60f);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}