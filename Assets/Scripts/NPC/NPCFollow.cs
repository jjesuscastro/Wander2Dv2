using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    Transform tran;

    public float speed = 40f;

    void Start()
    {
        tran = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(tran.position, target.position) > 3)
            tran.position = Vector2.MoveTowards(tran.position, target.position, speed * Time.deltaTime);
    }
}
