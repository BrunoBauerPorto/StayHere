using StayHereCamera;
using UnityEngine;
using UnityEngine.Video;

public class TVPlay : MonoBehaviour
{
    public VideoPlayer tvPlay;

    public bool playerInside;

    void Awake()
    {
        if (tvPlay == null)
            tvPlay = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        ControlCamera.OnActiveCameraChanged += HandleCameraChanged;
    }

    void OnDisable()
    {
        ControlCamera.OnActiveCameraChanged -= HandleCameraChanged;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = true;
        UpdatePlayback();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        StopVideo();
    }

    void HandleCameraChanged(bool isAtA)
    {
        UpdatePlayback();
    }

    void UpdatePlayback()
    {
        // ajuste aqui: "câmera que carrega" = A (isAtA == true)
        if (playerInside && ControlCamera.isAtA == false)
            PlayVideo();
        else
            StopVideo();
    }

    void PlayVideo()
    {
        if (tvPlay == null) return;
        if (!tvPlay.isPlaying) tvPlay.Play();
    }

    void StopVideo()
    {
        if (tvPlay == null) return;
        if (tvPlay.isPlaying) tvPlay.Stop();
    }
}
