using UnityEngine;
using System.Collections;


public class CameraMove : MonoBehaviour 
{
    private Vector3 camPos;
    public static float cameraSpeed=0f;
    private Vector3 velocity = Vector3.zero;


    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(3);
        cameraSpeed = 2f;
    }

    void Start()
    {
        StartCoroutine(WaitAtStart());
    }
    void Update () 
    {
        camPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(0f, camPos.y + cameraSpeed * Time.deltaTime, -10f);
    }

    /*void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            Vector3 smooth = Vector3.SmoothDamp(transform.position, newPos,ref velocity, 10f*Time.deltaTime);
            transform.position = newPos;
        }
    }*/

    public static void IncreaseCameraSpeed()
    {
        if (cameraSpeed < 18f)
        {
            cameraSpeed += 1f;
        }
    }

    public static void ResetCameraSpeed()
    {
     
            cameraSpeed = 0f;
        
    }

    public float getCameraSpeed()
    {
        return cameraSpeed;
    }
}
