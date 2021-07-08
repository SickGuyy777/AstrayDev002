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
            animated.PlayAnimation(0, true);
        else
            animated.PlayAnimation(1, true);

        // Rotation
        if (rotationMode == RotationMode.ToTarget)
            rotationTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Interaction
        bool interact = Input.GetButton("Interact");

        if (interact)
        {
            Interactable interactable = _interactor.GetSelectedInteractable();
            if (interactable is Pickupable pickupable)
            {
                pickupable.Interact(_interactor, (Vector2)transform.position + (Vector2)transform.up,
                        transform.rotation, transform);
            }
        }
    }
}
