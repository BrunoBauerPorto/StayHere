using UnityEngine;

public class CameraCollect : Interactable
{
    public GameObject camPlayer;
    public static bool isActiveCam;


    protected override void interact()
    {
        camPlayer.SetActive(true);
        isActiveCam = true; 
        gameObject.SetActive(false);
    }
}
