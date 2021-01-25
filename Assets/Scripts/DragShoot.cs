using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragShoot : MonoBehaviour
{
    private bool dragging = false;

    public float power;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endpoint;

    private TrajectoryLine tl;

    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
    }

    private void Update()
    {
        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            //if (touch.phase == TouchPhase.Began)
            //{
            //    startPoint = cam.ScreenToWorldPoint(touch.position);
            //    startPoint.z = 15;
            //    Debug.Log(startPoint);
            //}

            //if (touch.phase == TouchPhase.Moved)
            //{
            //    Vector3 currentPoint = cam.ScreenToWorldPoint(touch.position);
            //    currentPoint.z = 15;
            //    tl.RenderLine(startPoint, currentPoint);
            //}

            //if (touch.phase == TouchPhase.Ended)
            //{
            //    endpoint = cam.ScreenToWorldPoint(touch.position);
            //    endpoint.z = 15;
            //    Debug.Log(endpoint);

            //    force = new Vector2(Mathf.Clamp(startPoint.x - endpoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endpoint.y, minPower.y, maxPower.y));
            //    rb.AddForce(force * power, ForceMode2D.Impulse);
            //    tl.EndLine();
            //}

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPoint = cam.ScreenToWorldPoint(touch.position);
                    startPoint.z = 15;
                    Debug.Log(startPoint);
                    dragging = true;
                    break;

                case TouchPhase.Moved:
                    if (dragging)
                    {
                        Vector3 currentPoint = cam.ScreenToWorldPoint(touch.position);
                        currentPoint.z = 15;
                        tl.RenderLine(startPoint,currentPoint);
                    }
                    break;

                case TouchPhase.Ended:
                    endpoint = cam.ScreenToWorldPoint(touch.position);
                    endpoint.z = 15;
                    Debug.Log(endpoint);

                    force = new Vector2(Mathf.Clamp(startPoint.x - endpoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endpoint.y, minPower.y, maxPower.y));
                    rb.AddForce(force * power, ForceMode2D.Impulse);
                    tl.EndLine();
                    dragging = false;
                    break;
            }


        }

    }


    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector3 currentPoint = cam.ScreenToWorldPoint(eventData.pointerCurrentRaycast.worldPosition);
    //    currentPoint.z = 15;
    //    tl.RenderLine(startPoint, endpoint);    
    //}
}
