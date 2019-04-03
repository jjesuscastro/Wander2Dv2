using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderRandom : MonoBehaviour
{
    public Transform thunderManager;
    public GameObject[] thunders;
    public float minX;
    public float maxX;
    public float fixedY;
    public float minInterval;
    public float maxInterval;

    // Update is called once per frame
    void Start()
    {
        Invoke("SpawnThunder", 0);
    }

    void SpawnThunder()
    {
        float interval = Random.Range(minInterval, maxInterval);
        float x = Random.Range(minX, maxX);
        Vector3 camera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        int objToSpawn = Random.Range(0, thunders.Length);

        GameObject newObj = Instantiate(thunders[objToSpawn], new Vector3(x, camera.y/2.5f, 0), Quaternion.identity);
        newObj.transform.SetParent(thunderManager);

        Debug.Log("Interval = " + interval);
        Invoke("SpawnThunder", interval);
    }
}
