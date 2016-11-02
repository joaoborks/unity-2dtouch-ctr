using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour
{
    public Rope[] ropes;

	void Awake()
    {
        foreach (Rope rope in ropes)
            rope.AttachCandy(transform);
    }

    public void GetEaten()
    {
        gameObject.SetActive(false);
        foreach (Rope rope in ropes)
            if (!rope.broken)
                rope.Break(transform.position, false);
    }
}