using UnityEngine;


[System.Serializable]
public struct CameraData
{
    public float height;   
    public float distance; 
}


public class CamCustom : MonoBehaviour
{
    public CameraData truck;
    public CameraData trailer;
    RCC_Camera cam; 

    private void Start()
    {
        cam = RCC_SceneManager.Instance.activePlayerCamera;
    }
    public void setcamTruck()
    {
        cam.TPSDistance = truck.distance;
        cam.TPSHeight = truck.height;
    }
    
    public void setcamTrailer()
    {
        cam.TPSDistance = trailer.distance;
        cam.TPSHeight = trailer.height;
    }
}
