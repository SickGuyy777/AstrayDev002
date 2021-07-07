using System.Collections;
using UnityEngine;

/// <summary>
/// BASE - A base class, able to interact with <see cref="Interactable"/>s.
/// </summary>
public class Interactor : MonoBehaviour
{
    [Tooltip("The cooldown for searching for interactables")]
    [Range(0.00001f, 10f)]
    [SerializeField]
    protected float searchCooldown = 0.02f;

    [Tooltip("The mask to search interactables on")]
    [SerializeField]
    protected LayerMask searchMask;

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
    protected virtual Interactable GetSelectedInteractable()
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
}
