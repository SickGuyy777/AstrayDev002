using UnityEngine;

/// <summary>
/// A CharacterController that works with player input.
/// </summary>
[RequireComponent(typeof(Interactor))]
public class PlayerController : CharacterController
{
    [SerializeField]
    private AnimatedObject animated;

    private Interactor _interactor;

    protected override void OnAwake() => _interactor = GetComponent<Interactor>();

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(horizontalInput, verticalInput);

        if (MoveDirection != Vector2.zero)
            animated.PlayAnimation(0);
        else
            animated.StopAnimation();

        // Rotation
        if (rotationMode == RotationMode.ToTarget)
            rotationTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Interaction
        bool interact = Input.GetButton("Interact");

        if (interact)
            _interactor.Interact();
    }
}
