using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public bool normalAttackInput { get; private set; }
    public void OnRawMovementInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
    }
    public void OnNormalAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalAttackInput = true;
        }
        else if (context.canceled)
        {
            normalAttackInput = false;
        }
    }
}
