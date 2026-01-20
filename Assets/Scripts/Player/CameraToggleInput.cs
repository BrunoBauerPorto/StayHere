using UnityEngine;
using UnityEngine.InputSystem;

namespace StayHereCamera
{
    public class CameraToggleInput : MonoBehaviour
    {
        [SerializeField] ControlCamera cameraToggle;

        void OnValidate()
        {
            if (cameraToggle == null)
                cameraToggle = FindFirstObjectByType<ControlCamera>();
        }

        // CHAMADO AUTOMATICAMENTE PELO PlayerInput (Send Messages)
        void OnToggleCam(InputValue button)
        {
            if(CameraCollect.isActiveCam == true)
            {
                // garante que só dispara quando o botão é pressionado (não no release)
                if (!button.isPressed) return;

                if (cameraToggle != null)
                    cameraToggle.ToggleCamera();
            }

        }
    }
}

