using UnityEngine;
using System.Collections;

public class BottomLine : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 size;
    public LayerMask layer;

    Frog frog;

    void Awake()
    {
        frog = FindObjectOfType<Frog>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox(transform.position + (Vector3)offset, size, 0, layer))
        {
            frog.Lose();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, size);
    }
}