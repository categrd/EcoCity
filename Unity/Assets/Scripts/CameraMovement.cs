using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class CameraMovement : MonoBehaviour
    {
        public Camera gameCamera;
        public float cameraMovementSpeed = 5;
        public float minPossibleZoom, maxPossibleZoom;
        private void Start()
        {
            gameCamera = GetComponent<Camera>();
            
        }
        public void MoveCamera(Vector3 inputVector)
        {
            var movementVector = Quaternion.Euler(0,30,0) * inputVector;
            gameCamera.transform.position += Time.deltaTime * cameraMovementSpeed * movementVector;
        }

        public void ZoomCamera(float zoom)
        {
            float newSize = gameCamera.orthographicSize - zoom;

            // Use epsilon to account for floating-point imprecisions
            if (newSize >= minPossibleZoom - float.Epsilon && newSize <= maxPossibleZoom + float.Epsilon)
            {
                gameCamera.orthographicSize = newSize;
            }

            // Limit the zoom after the adjustment
            LimitZoomCamera();
        }

        public void LimitZoomCamera()
        {
            if (gameCamera.orthographicSize < minPossibleZoom)
                gameCamera.orthographicSize = minPossibleZoom;
            if (gameCamera.orthographicSize > maxPossibleZoom)
                gameCamera.orthographicSize = maxPossibleZoom;
        }
    }
