using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object;

public class TreeHouse : MonoBehaviour
{
    BlankPickup dialogue;
    public UnityEvent onFinish;
    [SerializeField]
    Transform leftSide;
    [SerializeField]
    Transform rightSide;

    [SerializeField]
    TreeHousePlatform leftPlatform;
    [SerializeField]
    TreeHousePlatform rightPlatform;

    [SerializeField]
    PositionLimits leftRock;
    [SerializeField]
    PositionLimits rightRock;

    public GameObject walls;

    public float leftY;
    public float rightY;

    void Start()
    {
        dialogue = gameObject.GetComponentInChildren<BlankPickup>();
        //test
        leftY = leftSide.localPosition.y;
        rightY = rightSide.localPosition.y;

        leftPlatform = leftSide.gameObject.GetComponent<TreeHousePlatform>();
        rightPlatform = rightSide.gameObject.GetComponent<TreeHousePlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempVector;

        if(leftPlatform.hasObject)
        { 
            tempVector = leftSide.localPosition;
            if(tempVector.y > 8)
                tempVector.y -= 0.1f;
            else
            {
                leftRock.limitX = false;
            }
            leftSide.localPosition = tempVector;

            tempVector = rightSide.localPosition;
            if(tempVector.y < 18.14)
                tempVector.y += 0.1f;
            
            if(leftPlatform.hasPlayer)
            {
                rightRock.minX = 930.9585f;
                dialogue.ManualDestroy();
            }
            rightSide.localPosition = tempVector;
        }

        if(rightPlatform.hasObject && leftPlatform.hasPlayer)
        {
            tempVector = leftSide.localPosition;
            if(tempVector.y < 22.09)
            {
                tempVector.y += 0.1f;
                walls.SetActive(true);
            } else {
                if(onFinish != null)
                    onFinish.Invoke();
            }
            leftSide.localPosition = tempVector;

            tempVector = rightSide.localPosition;
            if(tempVector.y > 8)
                tempVector.y -= 0.1f;
                
            rightSide.localPosition = tempVector;
        }
    }
}
