using UnityEngine;
using System.Collections;

public class BottomLine : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 size;
    public LayerMask layer;

    Frog frog;

    Vector2 boxPos
    {
        get { return (Vector2)transform.position + offset; }
    }

    void Awake()
    {
        frog = FindObjectOfType<Frog>();
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapBox(boxPos, size, 0, layer))
            frog.Lose();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxPos, size);
    }
}