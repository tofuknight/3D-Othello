using UnityEngine;
using System.Collections;

/// <summary>
/// 鼠标控制相机旋转
/// </summary>
public class CameraController : MonoBehaviour
{
    public GameObject camera;
    public GameObject UI;
    enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
    RotationAxes axes = RotationAxes.MouseXAndY;

    float sensitivityX = 10;
    float sensitivityY = 10;

    float minimumY = -80;
    float maximumY = 80;
    private float rotationY = 0;

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = Camera.main.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                Camera.main.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                Camera.main.transform.localEulerAngles = new Vector3(-rotationY, Camera.main.transform.localEulerAngles.y, 0);
            }
        }
    }

    public void gameStart()
    {
        camera.SetActive(false);
        UI.SetActive(false);
    }
} 