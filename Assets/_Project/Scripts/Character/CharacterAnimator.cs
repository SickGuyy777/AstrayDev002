using UnityEngine;

[RequireComponent(typeof(AnimatedObject))]
public class CharacterAnimator : MonoBehaviour
{
    protected virtual void OnAwake() { }
    protected virtual void OnUpdate() { }

    protected AnimatedObject _animated;

    [SerializeField]
    protected CharacterController _controller;

    protected void Awake()
    {
        _animated = GetComponent<AnimatedObject>();

        if (!_controller)
            _controller = GetComponent<CharacterController>();

        if (!_controller)
            Debug.LogError("Controller is null", gameObject);

        OnAwake();
    }

    protected virtual void Update()
    {
        if (_controller.IsMoving)
            _animated.PlayAnimation("Walk", true);
        else
            _animated.PlayAnimation("Idle", true);

        OnUpdate();
    }
}
