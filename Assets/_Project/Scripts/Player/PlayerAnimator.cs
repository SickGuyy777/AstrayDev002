using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    [SerializeField]
    protected AnimatedObject handsAnimated;

    protected override void OnUpdate()
    {
        if (_controller.IsMoving)
            handsAnimated.PlayAnimation("Walk", true);
        else
            handsAnimated.PlayAnimation("Idle", true);
    }
}
