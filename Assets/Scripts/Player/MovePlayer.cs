using UnityEngine;
using Unity.Cinemachine;

namespace StayHere.Move
{
    [RequireComponent(typeof(CharacterController))]
    public class MovePlayer : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float maxSpeed = 3.5f;
        public float acceleration = 12f;     // valores mais altos costumam ficar melhores
        public float deceleration = 14f;

        public Vector3 currentVelocity { get; private set; }
        public float currentSpeed { get; private set; }

        [Header("Gravity")]
        public float gravity = -9.81f;
        public float groundedStickForce = -2f; // mantém colado no chão
        float verticalVelocity;

        [Header("Look Parameters")]
        public Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
        public float pitchLimit = 85f;

        [SerializeField] float currentPitch = 0f;
        public float CurrentPitch
        {
            get => currentPitch;
            set => currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }

        [Header("Input")]
        public Vector2 MoveInput;
        public Vector2 LookInput;

        [Header("Components")]
        [SerializeField] CinemachineCamera fpCamera;
        [SerializeField] CharacterController characterController;

        private void OnValidate()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MoveUpdate();
            LookUpdate();
        }

        void MoveUpdate()
        {
            // Direção correta: forward/right (não position)
            Vector3 moveDir = (transform.forward * MoveInput.y) + (transform.right * MoveInput.x);
            moveDir.y = 0f;

            // evita speed maior na diagonal
            if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

            Vector3 targetHorizontalVelocity = moveDir * maxSpeed;

            float accel = (moveDir.sqrMagnitude > 0.001f) ? acceleration : deceleration;

            // acelera/desacelera só no plano XZ
            Vector3 horizontal = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
            horizontal = Vector3.MoveTowards(horizontal, targetHorizontalVelocity, accel * Time.deltaTime);

            // gravidade
            if (characterController.isGrounded)
            {
                if (verticalVelocity < 0f)
                    verticalVelocity = groundedStickForce;
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            currentVelocity = new Vector3(horizontal.x, verticalVelocity, horizontal.z);
            currentSpeed = new Vector3(currentVelocity.x, 0f, currentVelocity.z).magnitude;

            characterController.Move(currentVelocity * Time.deltaTime);
        }

        void LookUpdate()
        {
            Vector2 input = new Vector2(LookInput.x * lookSensitivity.x, LookInput.y * lookSensitivity.y);

            CurrentPitch -= input.y;

            if (fpCamera != null)
                fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

            transform.Rotate(Vector3.up * input.x);
        }
    }
}
