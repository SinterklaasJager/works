using UnityEngine;
using UnityEngine.InputSystem;


namespace MovementControlls
{
    public class PlayerInput : MonoBehaviour
    {
        public float MoveAxisDeadZone = 0.2f;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LastMoveInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool JumpInput { get; private set; }

        public bool HasMoveInput { get; private set; }
        PlayerControls controls;

        Vector2 move;


        public GameObject MainCamera;

        void Awake()
        {
            controls = new PlayerControls();

            // controls.Gameplay.Jump.performed += ctx => Jump();

            controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
            controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;

            // controls.Gameplay.CameraReset.performed += ctx => CameraReset();

        }

        public void UpdateInput()
        {
            // Update MoveInput
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(moveInput.x) < MoveAxisDeadZone)
            {
                moveInput.x = 0.0f;
            }

            if (Mathf.Abs(moveInput.y) < MoveAxisDeadZone)
            {
                moveInput.y = 0.0f;
            }

            bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

            if (HasMoveInput && !hasMoveInput)
            {
                LastMoveInput = MoveInput;
            }

            MoveInput = moveInput;
            HasMoveInput = hasMoveInput;

            // Update other inputs
            CameraInput = new Vector2(-Input.GetAxis("Move X"), Input.GetAxis("Move Y"));
            JumpInput = Input.GetButton("Jump");
        }
    }
}
