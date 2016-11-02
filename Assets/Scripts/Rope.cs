using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    Transform fragment,
        candy,
        tip;
    LineRenderer line;
    EdgeCollider2D col;
    DistanceJoint2D dist;
    RaycastHit2D[] hit;
    bool broken;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        dist = GetComponent<DistanceJoint2D>();
        fragment = transform.GetChild(0);
        tip = transform.GetChild(1);
    }

    void Update()
    {
        if (!broken)
            ResizeElements(candy.position);
        else
            ResizeElements(tip.transform.position);
    }

    void ResizeElements(Vector3 point)
    {
        Vector3 localPoint = transform.InverseTransformPoint(point);

        // Resize Edge Collider
        Vector2[] colPoints = col.points;
        colPoints[1] = localPoint;
        col.points = colPoints;

        // Resize Line Renderer
        line.SetPosition(1, localPoint);
    }

    public void AttachCandy(Transform candy)
    {
        this.candy = candy;
    }

    public void Break(Vector2 point)
    {
        if (broken)
            return;

        ResizeElements(point);

        // Cut and let the tip active to simulate physics
        tip.transform.position = point;
        tip.gameObject.SetActive(true);
        dist.connectedBody = tip.GetComponent<Rigidbody2D>();
        dist.anchor = Vector2.zero;
        dist.distance = Vector2.Distance(point, transform.position);

        // The remaining rope should stick with the candy
        fragment.SetParent(candy, false);
        fragment.transform.localPosition = Vector2.zero;
        fragment.gameObject.SetActive(true);
        fragment.GetComponent<RopeFragment>().Detach(point);

        // Prevent from breaking again
        broken = true;
    }
}