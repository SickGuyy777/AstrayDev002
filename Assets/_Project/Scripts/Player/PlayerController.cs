using UnityEngine;

/// <summary>
/// A CharacterController that works with player input.
/// </summary>
[RequireComponent(typeof(Interactor))]
public class PlayerController : CharacterController
{
    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(horizontalInput, verticalInput);

        // Rotation
        if (rotationMode == RotationMode.ToTarget)
            rotationTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
