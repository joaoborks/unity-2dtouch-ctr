using UnityEngine;
using System.Collections;

public class RopeFragment : MonoBehaviour
{
    Transform tip;
    LineRenderer line;
    HingeJoint2D joint;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        joint = GetComponent<HingeJoint2D>();
        tip = transform.GetChild(0);
    }

    void Update()
    {
        line.SetPosition(1, tip.localPosition);
    }

	public void Detach(Vector2 point)
    {
        tip.position = point;
        joint.connectedAnchor = -transform.InverseTransformPoint(point);
    }
}