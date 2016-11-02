using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public LayerMask mask;

    Touch t;
    Camera cam;
    Vector2 mousePos;
    Vector2 lastMousePos;
    Vector2[] inputs;
    Transform inputPos;
    RaycastHit2D[] hits = new RaycastHit2D[3];

    void Awake()
    {
        inputs = new Vector2[2];
        cam = Camera.main;
        inputPos = transform.GetChild(0);
        Input.simulateMouseWithTouches = false;
    }

    void FixedUpdate()
    {
        if (Input.touchSupported)
        {
            t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                inputs[0] = cam.ScreenToWorldPoint(t.position - t.deltaPosition);
                inputs[1] = cam.ScreenToWorldPoint(t.position);
                inputPos.position = inputs[1];
                SwipeCollisionDetect();
            }
        }
        else
        {
            mousePos = Input.mousePosition;
            if (Input.GetMouseButton(0) && mousePos != lastMousePos)
            {
                inputs[0] = cam.ScreenToWorldPoint(lastMousePos);
                inputs[1] = cam.ScreenToWorldPoint(mousePos);
                inputPos.position = inputs[1];        
                SwipeCollisionDetect();
            }
            lastMousePos = mousePos;
        }
    }

    void SwipeCollisionDetect()
    {
        int detections = Physics2D.LinecastNonAlloc(inputs[0], inputs[1], hits, mask);
        if (detections > 0)
        {
            for (int i = 0; i < detections; i++)
                hits[i].collider.GetComponent<Rope>().Break(hits[i].point);
        }
    }
}