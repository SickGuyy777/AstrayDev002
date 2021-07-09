using UnityEngine;

/// <summary>
/// BASE - A base class for character movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// Called when <see cref="Awake"/> is called.
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// Called when <see cref="FixedUpdate"/> is called.
    /// </summary>
    protected virtual void OnFixedUpdate() { }

    /// <summary>
    /// Get <see cref="rb"/>.
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        OnAwake();
    }

    // Responsible for movement logic
    #region Movement
    [Header("Movement")]

    [Tooltip("The maximum velocity of the character")]
    [Range(0.25f, 12f)]
    [SerializeField]
    private float maxVelocity = 6.0f;

    [Tooltip("The speed the character accelerates at")]
    [Range(0.00001f, 0.75f)]
    [SerializeField]
    private float accelerationSpeed = 0.2f;

    [Tooltip("The speed the character decelerates at")]
    [Range(0.00001f, 0.75f)]
    [SerializeField]
    private float decelerationSpeed = 0.2f;

    [Tooltip("A vector representing the normalized direction of the character")]
    [Readonly]
    [SerializeField]
    private Vector2 _moveDirection;
    /// <summary>
    /// A vector representing the normalized direction of the character
    /// </summary>
    public Vector2 MoveDirection
    {
        get { return _moveDirection; }
        set
        {
            value.Normalize();

            if (value == _moveDirection)
                return;

            MoveSpeed = value * maxVelocity;
            _moveDirection = value;
        }
    }

    /// <summary>
    /// A vector representing the speed of the character
    /// </summary>
    public Vector2 MoveSpeed { get; private set; }

    /// <summary>
    /// Is the character moving?
    /// </summary>
    public bool IsMoving => MoveSpeed != Vector2.zero;

    /// <summary>
    /// The character's Rigidbody2D
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Move the character with <see cref="MoveSpeed"/>.
    /// </summary>
    private void Move()
    {
        if (MoveDirection != Vector2.zero) // Accelerate/Move
        {
            if (rb.velocity.magnitude < maxVelocity) // Accelerating
                rb.velocity = Vector2.MoveTowards(rb.velocity, MoveSpeed, accelerationSpeed);
            else // Move
                rb.velocity = MoveSpeed;
        }
        else // Decelerate/Stop
        {
            if (rb.velocity.magnitude > 0) // Decelerate
                rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, decelerationSpeed);
         // else
            // Stop
        }
    }
    #endregion

    [Space]

    // Responsible for rotation logic
    #region Rotation
    [Header("Rotation")]

    [Tooltip("True: lerping the rotation. False: setting the rotation to the DesiredRotation")]
    [SerializeField]
    private bool lerpRotation = false;

    [Tooltip("The speed of rotation")]
    [SerializeField]
    private float rotationSpeed = 1.0f;

    /// <summary>
    /// The position to rotate to
    /// </summary>
    protected Vector2 rotationTarget;
    /// <summary>
    /// The desired rotation as a quaternion
    /// </summary>
    protected Quaternion DesiredRotation
    {
        get
        {
            Vector2 direction = rotationTarget - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
    }

    [Tooltip("The mode of rotation RotationMode")]
    [SerializeField]
    protected RotationMode rotationMode;

    /// <summary>
    /// Roatate using <see cref="rotationMode"/>.
    /// </summary>
    private void UpdateRotation()
    {
        if (rotationMode == RotationMode.ToMovement && MoveDirection != Vector2.zero)
            rotationTarget = (Vector2)transform.position + MoveSpeed;

        if (lerpRotation)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, DesiredRotation, rotationSpeed);
        else
            transform.localRotation = DesiredRotation;
    }

    /// <summary>
    /// ToMovement - rotate to the movement direction
    /// ToTarget - rotate to <see cref="rotationTarget"/>.
    /// </summary>
    protected enum RotationMode
    {
        ToTarget, ToMovement
    }
    #endregion

    /// <summary>
    /// Moving and Rotating the character.
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void FixedUpdate()
    {
        Move();
        UpdateRotation();
        OnFixedUpdate();
    }
}
