using UnityEngine;
using StayHere.Move;
using UnityEngine.InputSystem;

namespace StayHere.Player
{
    [RequireComponent(typeof(MovePlayer))]
    public class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] MovePlayer movePlayer;

        void OnMove(InputValue value)
        {
            if (movePlayer == null) return;
            movePlayer.MoveInput = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            if (movePlayer == null) return;
            movePlayer.LookInput = value.Get<Vector2>();
        }

        private void OnValidate()
        {
            if (movePlayer == null)
                movePlayer = GetComponent<MovePlayer>();
        }

        private void Start()
        {
            LockCursor();
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
