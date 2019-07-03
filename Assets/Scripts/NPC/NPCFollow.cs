using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    Transform transform;

    public float speed = 40f;

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 3)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
