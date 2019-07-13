using UnityEngine;
using System.Collections;
using Player;

namespace GameManager
{
    public class CameraFollow : MonoBehaviour
    {
        public PlayerMovement target;
        public float verticalOffset;
        public float lookAheadDstX;
        public float lookSmoothTimeX;
        public float verticalSmoothTime;
        public float maxX;
        public float minX;
        public Vector2 focusAreaSize;

        FocusArea focusArea;

        float currentLookAheadX;
        float targetLookAheadX;
        float lookAheadDirX;
        float smoothLookVelocityX;
        float smoothVelocityY;

        // Vector3 initialPosition;

        bool lookAheadStopped;

        #region Singleton
        public static CameraFollow instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple camera follows found");
            }
            instance = this;
        }
        #endregion

        void Start()
        {
            SetLevel();
            // initialPosition = transform.position;
            // minX = initialPosition.x;
            focusArea = new FocusArea(target.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
        }

        void LateUpdate()
        {
            // print(transform.position.x);
            focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);

            Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
                {
                    lookAheadStopped = false;
                    targetLookAheadX = lookAheadDirX * lookAheadDstX;
                }
                else
                {
                    if (!lookAheadStopped)
                    {
                        lookAheadStopped = true;
                        targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                    }
                }
            }

            currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

            focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
            focusPosition.x = Mathf.SmoothDamp(focusPosition.x, Mathf.Clamp(focusPosition.x, minX, maxX), ref smoothLookVelocityX, lookSmoothTimeX);
            focusPosition += Vector2.right * currentLookAheadX;
            transform.position = (Vector3)focusPosition + Vector3.forward * -10;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, .5f);
            Gizmos.DrawCube(focusArea.centre, focusAreaSize);
        }

        public void SetLevel()
        {
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].name.CompareTo("mc") == 0)
                    target = players[i];
            }
        }

        public void switchTarget(PlayerMovement newTarget)
        {
            target = newTarget;
        }

        public void setMinX(float newMinX)
        {
            minX = newMinX;
        }

        public void setMaxX(float newMaxX)
        {
            maxX = newMaxX;
        }

        struct FocusArea
        {
            public Vector2 centre;
            public Vector2 velocity;
            float left, right;
            float top, bottom;


            public FocusArea(Bounds targetBounds, Vector2 size)
            {
                left = targetBounds.center.x - size.x / 2;
                right = targetBounds.center.x + size.x / 2;
                bottom = targetBounds.min.y;
                top = targetBounds.min.y + size.y;

                velocity = Vector2.zero;
                centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            }

            public void Update(Bounds targetBounds)
            {
                float shiftX = 0;
                if (targetBounds.min.x < left)
                {
                    shiftX = targetBounds.min.x - left;
                }
                else if (targetBounds.max.x > right)
                {
                    shiftX = targetBounds.max.x - right;
                }
                left += shiftX;
                right += shiftX;

                float shiftY = 0;
                if (targetBounds.min.y < bottom)
                {
                    shiftY = targetBounds.min.y - bottom;
                }
                else if (targetBounds.max.y > top)
                {
                    shiftY = targetBounds.max.y - top;
                }
                top += shiftY;
                bottom += shiftY;
                centre = new Vector2((left + right) / 2, (top + bottom) / 2);
                velocity = new Vector2(shiftX, shiftY);
            }
        }
    }
}