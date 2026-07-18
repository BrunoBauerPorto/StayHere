using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float distance = 5f;
    [SerializeField] private LayerMask mask;

    private PlayerInput playerInput;
    private InputAction interactAction;

  

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        interactAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        interactAction?.Enable();
    }

    private void OnDisable()
    {
        interactAction?.Disable();
    }

    private void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, mask))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            var door = hit.collider.GetComponent<DoorMath>();
            if (interactable != null)
            {
                if (interactAction.WasPressedThisFrame())
                {
                    interactable.BaseInteract();
                }
            }

            if (door != null)
            {
                if (interactAction.WasPressedThisFrame())
                {
                    Debug.Log("Open");
                    door.InteractWithDoor(transform);
                }
            }
        }


    }
}
