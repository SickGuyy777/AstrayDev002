using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// BASE - A base class, able to interact with <see cref="Interactable"/>s.
/// </summary>
public class Interactor : MonoBehaviour
{
    /// <summary>
    /// Called when <see cref="Start"/> is called.
    /// </summary>
    protected virtual void OnStart() { }

    /// <summary>
    /// The cooldown for searching for interactables
    /// </summary>
    [field: Tooltip("The cooldown for searching for interactables")]
    [field: Range(0.00001f, 10f)]
    [field: SerializeField]
    public float SearchCooldown { get; private set; } = 0.02f;

    [Tooltip("The mask to search interactables on")]
    [SerializeField]
    protected LayerMask searchMask;

    protected List<Interactable> hoveredInteractables = new List<Interactable>();

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Start()
    {
        StartCoroutine(SearchForInteractables());
        OnStart();
    }

    /// <summary>
    /// Interact with <see cref="GetSelectedInteractable()"/>
    /// </summary>
    /// <returns>The interactable <see cref="Interactor"/> has interacted with</returns>
    public Interactable Interact()
    {
        Interactable selected = GetSelectedInteractable();
        if (selected)
            selected.Interact(this);

        return selected;
    }

    /// <summary>
    /// Get the selected interactable.
    /// </summary>
    /// <returns>The selected interactable</returns>
    public virtual Interactable GetSelectedInteractable()
    {
        Debug.LogException(new System.NotImplementedException($"GetSelectedInteractable() not implemented in {name}"), gameObject);
        return null;
    }

    /// <summary>
    /// Search for interactables.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator SearchForInteractables()
    {
        Debug.LogException(new System.NotImplementedException($"SearchForInteractables() not implemented in {name}"), gameObject);
        yield return null;
    }

    protected void ClearHovered()
    {
        if (hoveredInteractables.Count == 0)
            return;

        foreach (var hovered in hoveredInteractables.ToList())
            StopHoveringInteractable(hovered);

        hoveredInteractables.Clear();
    }

    /// <summary>
    /// Start hovering <paramref name="interactable"/>.
    /// </summary>
    /// <param name="interactable">The interactable to start hovering.</param>
    /// <returns>Whether the interactor was able to hover <paramref name="interactable"/>.</returns>
    protected bool StartHoveringInteractable(Interactable interactable)
    {
        if (!IsHoveringInteractable(interactable))
        {
            interactable.Hover(this);
            hoveredInteractables.Add(interactable);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Stop hovering <paramref name="interactable"/>.
    /// </summary>
    /// <param name="interactable">The interactable to stop hovering</param>
    /// <returns>Whether the interactor was able to stop hovering <paramref name="interactable"/></returns>
    protected bool StopHoveringInteractable(Interactable interactable)
    {
        if (IsHoveringInteractable(interactable))
        {
            hoveredInteractables.Remove(interactable);
            interactable.StopHover(this);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Is <see langword="this"/> <see cref="Interactor"/> is hovering <paramref name="interactable"/>.
    /// </summary>
    /// <param name="interactable">The interactable to check</param>
    /// <returns>If <see langword="this"/> <see cref="Interactor"/> is hovering <paramref name="interactable"/></returns>
    protected bool IsHoveringInteractable(Interactable interactable) =>
            hoveredInteractables.Contains(interactable);
}
