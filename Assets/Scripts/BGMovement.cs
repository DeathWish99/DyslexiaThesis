using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float offset;
    private Vector2 startPos;
    private float newXpos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        newXpos = Mathf.Repeat(Time.time * -moveSpeed, offset);
        transform.position = startPos + Vector2.right * newXpos;
    }
}
