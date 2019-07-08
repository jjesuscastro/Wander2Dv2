using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHouse : MonoBehaviour
{
    [SerializeField]
    Transform leftSide;
    [SerializeField]
    Transform rightSide;

    [SerializeField]
    TreeHousePlatform leftPlatform;
    [SerializeField]
    TreeHousePlatform rightPlatform;

    [SerializeField]
    PositionLimits rightRock;

    public float leftY;
    public float rightY;

    void Start()
    {
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
                leftPlatform.hasObject = false;
            leftSide.localPosition = tempVector;

            tempVector = rightSide.localPosition;
            if(tempVector.y < 18.14)
                tempVector.y += 0.1f;
            else
                rightRock.minX = 930.9585f;
            rightSide.localPosition = tempVector;
        }

        if(rightPlatform.hasObject && leftPlatform.hasPlayer)
        {
            tempVector = leftSide.localPosition;
            if(tempVector.y < 22.09)
                tempVector.y += 0.1f;
            leftSide.localPosition = tempVector;

            tempVector = rightSide.localPosition;
            if(tempVector.y > 8)
                tempVector.y -= 0.1f;
                
            rightSide.localPosition = tempVector;
        }
    }
}
