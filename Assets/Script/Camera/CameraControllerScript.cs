using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    [SerializeField] private float cameraSpeed1; //slow
    [SerializeField] private float cameraSpeed2; //fast
    [SerializeField] private float borderThickness1 = 50f;
    [SerializeField] private float borderThickness2 = 10f;

    [SerializeField] private float cameraPositionLimitR;
    [SerializeField] private float cameraPositionLimitL;

    [SerializeField] private Camera mainCamera;

    private void Start()
    {
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 cameraMovement = Vector3.zero;

        //滑鼠是否抵達邊界
        if (mousePosition.x < borderThickness1)
        {
            if (mainCamera.transform.position.x > cameraPositionLimitL)
            {
                if (mousePosition.x < borderThickness2)
                {
                    cameraMovement.x -= cameraSpeed2;
                }
                else
                {
                    cameraMovement.x -= cameraSpeed1;
                }
            }
            
        }
        else if (mousePosition.x > Screen.width - borderThickness1)
        {
            if (mainCamera.transform.position.x < cameraPositionLimitR)
            {
                if (mousePosition.x > Screen.width - borderThickness2)
                {
                    cameraMovement.x += cameraSpeed2;
                }
                else
                {
                    cameraMovement.x += cameraSpeed1;
                }
            }
        }

        
        mainCamera.transform.Translate(cameraMovement * Time.deltaTime);
    }
}
