//	Created by: Sunny Valley Studio 
//	https://svstudio.itch.io

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

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
            if(gameCamera.orthographicSize - zoom > minPossibleZoom && gameCamera.orthographicSize + zoom < maxPossibleZoom )
                gameCamera.orthographicSize -= zoom;
            
        }
    }
}