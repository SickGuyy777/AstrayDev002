using UnityEngine;

[RequireComponent(typeof(AnimatedObject))]
public class CharacterAnimator : MonoBehaviour
{
    private AnimatedObject _animated;

    [SerializeField]
    private CharacterController _controller;

    private void Awake()
    {
        _animated = GetComponent<AnimatedObject>();

        if (!_controller)
            _controller = GetComponent<CharacterController>();

        if (!_controller)
            Debug.LogError("Controller is null", gameObject);
    }

    private void Update()
    {
        if (_controller.IsMoving)
            _animated.PlayAnimation("Walk", true);
        else
            _animated.PlayAnimation("Idle", true);
    }
}
