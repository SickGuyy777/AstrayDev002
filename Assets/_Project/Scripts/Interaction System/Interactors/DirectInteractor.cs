using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// An interactor with a radius search system
/// </summary>
public class DirectInteractor : Interactor
{
    [Tooltip("The radius to search")]
    [SerializeField]
    protected float searchRadius = 2.5f;

    /// <summary>
    /// See <see cref="Interactor"/>.
    /// </summary>
    /// <returns></returns>
    protected override Interactable GetSelectedInteractable()
    {
        if (hoveredInteractables.Count == 0)
            return null;

        if (hoveredInteractables.Count == 1)
            return hoveredInteractables[0];

        float minMagnitude = float.MaxValue;
        int selectedIndex = 0;

        for (int i = 1; i < hoveredInteractables.Count; i++)
        {
            float magnitude = ((Vector2)(hoveredInteractables[i].transform.position - transform.position)).magnitude;
            if (magnitude < minMagnitude)
            {
                minMagnitude = magnitude;
                selectedIndex = i;
            }
        }

        return hoveredInteractables[selectedIndex];
    }

    /// <summary>
    /// See <see cref="Interactor"/>
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator SearchForInteractables()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, searchMask);
            
            if (colliders.Length == 0)
            {
                ClearHovered();
                yield return new WaitForSeconds(SearchCooldown);
                continue;
            }

            List<Interactable> interactables = new List<Interactable>();

            foreach (var collider in colliders)
            {
                Interactable interactable = collider.GetComponent<Interactable>();
                if (!interactable)
                    continue;

                StartHoveringInteractable(interactable);

                interactables.Add(interactable);
            }
            foreach (var hovered in hoveredInteractables.ToList())
            {
                bool foundHovered = interactables.Contains(hovered);
                if (!foundHovered)
                    StopHoveringInteractable(hovered);
            }

            yield return new WaitForSeconds(SearchCooldown);
        }
    }
}
