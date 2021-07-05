using UnityEngine;

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
            if (MoveSpeed.magnitude < maxVelocity) // Accelerating
                rb.velocity = Vector2.MoveTowards(rb.velocity, MoveSpeed, accelerationSpeed);
            else // Move
                rb.velocity = MoveSpeed;
        }
        else // Decelerate/Stop
        {
            if (MoveSpeed.magnitude > 0) // Decelerate
                rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, decelerationSpeed);
         // else 
             // Stop
        }
    }
    #endregion

    // Responsible for rotation logic
    #region Rotation
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
            Quaternion result = transform.localRotation;

            Vector2 direction = rotationTarget - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x); // NOT in degrees. Using radience for less computing.
            result.z = angle;

            return result;
        }
    }

    /// <summary>
    /// The mode of rotation <see cref="RotationMode"/>
    /// </summary>
    protected RotationMode rotationMode;

    /// <summary>
    /// Roatate using <see cref="rotationMode"/>.
    /// </summary>
    private void UpdateRotation()
    {
        if (rotationMode == RotationMode.ToMovement)
            rotationTarget = (Vector2)transform.position + MoveDirection;

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
