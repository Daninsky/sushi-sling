using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragShoot : MonoBehaviour
{
    public ObjectContainer objectContainer;
    public GameObject nextDisplay;

    private bool dragging = false;
    private float dragTime;

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

            Vector3 touchPosWorld = cam.ScreenToWorldPoint(touch.position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            if (dragging) dragTime += Time.deltaTime;

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

            if (!dragging && Physics2D.Raycast(touchPosWorld2D, cam.transform.forward).collider != null && (Physics2D.Raycast(touchPosWorld2D, cam.transform.forward).collider.tag == "TouchArea" || Physics2D.Raycast(touchPosWorld2D, cam.transform.forward).collider.tag == "DamageZone"))
            {
                Debug.Log("nope");
            }
            else
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        nextDisplay.SetActive(false);
                        rb = objectContainer.GetActiveObject().GetComponent<Rigidbody2D>();
                        rb.gameObject.SetActive(true);
                        tl = rb.gameObject.GetComponent<TrajectoryLine>();
                        Debug.Log(rb);
                        rb.velocity = new Vector2(0, 0);
                        rb.angularVelocity = 0;
                        startPoint = cam.ScreenToWorldPoint(touch.position);
                        startPoint.z = -1;
                        rb.transform.position = startPoint;

                        dragging = true;
                        break;

                    case TouchPhase.Moved:
                        Vector3 currentPoint = cam.ScreenToWorldPoint(touch.position);
                        currentPoint.z = -1;
                        if (dragging)
                        {
                            tl.RenderLine(startPoint, currentPoint);
                            rb.transform.position = new Vector3(currentPoint.x, Mathf.Clamp(currentPoint.y,-4.5f,-1.5f), currentPoint.z);
                        }



                        break;

                    case TouchPhase.Ended:
                        nextDisplay.SetActive(true);
                        endpoint = cam.ScreenToWorldPoint(touch.position);
                        endpoint.z = -1;

                        if (Vector3.Distance(startPoint, endpoint) < 1.5)
                        {
                            rb.gameObject.SetActive(false);
                        }

                        else
                        {
                            force = new Vector2(Mathf.Clamp(startPoint.x - endpoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endpoint.y, minPower.y, maxPower.y));
                            rb.AddForce(force * power, ForceMode2D.Impulse);
                            tl.EndLine();

                            objectContainer.ChangeActivePooler();
                        }

                        Debug.Log(dragTime);
                        dragTime = 0;
                        dragging = false;

                        break;
                }
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
