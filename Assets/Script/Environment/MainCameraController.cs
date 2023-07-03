using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    Camera mainCamera;
    private Vector2Int minXY;
    private Vector2Int maxXY;

    public float cameraSpeed = 5f;
    public float screenEdgeBorder = 25f;
    private void Awake() {
        mainCamera = GetComponent<Camera>();
        CheckBoundary();
    }

    private void Update() {
        Boundary();
        CameraMove();
    }

    void CheckBoundary(){
        minXY = new Vector2Int(4, 22);
        maxXY = new Vector2Int(56, 27);
    }

    void Boundary(){
        
        Vector3 camPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        
        if(camPos.x < minXY.x){
            camPos.x = minXY.x;
        }
        else if(camPos.y < minXY.y){
            camPos.y = minXY.y;
        }
        else if(camPos.x > maxXY.x){
            camPos.x = maxXY.x;
        }
        else if(camPos.y > maxXY.y){
            camPos.y = maxXY.y;
        }
        
        mainCamera.transform.position = camPos;
        
    }

    void CameraMove(){
        Vector3 cameraMovement = Vector3.zero;

        // 마우스 위치를 스크린 좌표로 변환
        Vector3 mousePosition = Input.mousePosition;

        // 화면 테두리 체크 및 카메라 이동
        if (mousePosition.x < screenEdgeBorder)
            cameraMovement.x -= cameraSpeed;
        else if (mousePosition.x > Screen.width - screenEdgeBorder)
            cameraMovement.x += cameraSpeed;

        if (mousePosition.y < screenEdgeBorder)
            cameraMovement.y -= cameraSpeed;
        else if (mousePosition.y > Screen.height - screenEdgeBorder)
            cameraMovement.y += cameraSpeed;

        // 카메라 이동 적용
        transform.position += cameraMovement * Time.deltaTime;
    }


}
