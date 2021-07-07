using UnityEngine;

/// <summary>
/// BASE - A base class for interactables.
/// </summary>
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Is the <see cref="Interactable"/> hovered?
    /// </summary>
    public bool Hovered { get; private set; }

    /// <summary>
    /// Start hovering the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    public void Hover(Interactor interactor)
    {
        if (Hovered)
            return;

        Hovered = true;
        OnHover(interactor);
    }

    /// <summary>
    /// Stop hovering the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    public void StopHover(Interactor interactor)
    {
        if (!Hovered)
            return;

        Hovered = false;
        OnStopHover(interactor);
    }

    /// <summary>
    /// Interact with the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    public void Interact(Interactor interactor) => OnInteract(interactor);

    /// <summary>
    /// Called after <see cref="Hover(Interactor)"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnHover(Interactor interactor) { }
    /// <summary>
    /// Called after <see cref="StopHover(Interactor)"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnStopHover(Interactor interactor) { }
    /// <summary>
    /// Called after <see cref="Interact(Interactor)"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnInteract(Interactor interactor) { }
}
