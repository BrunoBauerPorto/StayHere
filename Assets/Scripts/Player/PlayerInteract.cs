using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float distance = 3f;
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
            if (interactable != null)
            {
                // "WasPressedThisFrame" evita ficar interagindo todo frame segurando o botão
                if (interactAction.WasPressedThisFrame())
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
