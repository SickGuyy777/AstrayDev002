﻿using System.Collections;
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
    /// Is the <see cref="Interactable"/> interacted?
    /// </summary>
    public bool Interacted { get; private set; }
    private bool interactFunction;

    /// <summary>
    /// Start hovering the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    public bool Hover(Interactor interactor)
    {
        if (!Hovered)
        {
            Hovered = true;
            OnHover(interactor);
        }

        return Hovered;
    }

    /// <summary>
    /// Stop hovering the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns>Whether <paramref name="interactor"/> stop hovering <see langword="this"/> <see cref="Interactable"/>.</returns>
    public bool StopHover(Interactor interactor)
    {
        if (Hovered)
        {
            Hovered = false;
            OnStopHover(interactor);
        }

        return !Hovered;
    }

    /// <summary>
    /// Interact with the <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    public void Interact(Interactor interactor)
    {
        interactFunction = true;

        if (!Interacted)
        {
            Interacted = true;
            OnStartInteract(interactor);
            StartCoroutine(InteractionUpdate(interactor));
        }
    }

    /// <summary>
    /// Called after <see cref="Hover(Interactor)"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnHover(Interactor interactor) { Debug.Log("Hovering"); }
    /// <summary>
    /// Called after <see cref="StopHover(Interactor)"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnStopHover(Interactor interactor) { Debug.Log("Stop Hovering"); }
    /// <summary>
    /// Called when <paramref name="interactor"/> starts interacting with <see langword="this"/> <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnStartInteract(Interactor interactor) { Debug.Log("Start Interacting"); }
    /// <summary>
    /// Called if <paramref name="interactor"/> is still interacting.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnInteract(Interactor interactor) { Debug.Log("Still Interacting"); }
    /// <summary>
    /// Called when <paramref name="interactor"/> stops interacting with <see langword="this"/> <see cref="Interactable"/>.
    /// </summary>
    /// <param name="interactor"></param>
    protected void OnStopInteract(Interactor interactor) { Debug.Log("Stop Interacting"); }

    /// <summary>
    /// Running the interaction update of <paramref name="interactor"/>.
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    private IEnumerator InteractionUpdate(Interactor interactor)
    {
        int timesNotInteracted = 0;
        while (true)
        {
            if (interactFunction) // Still interacting
            {
                interactFunction = false;
                timesNotInteracted = 0;
            }
            else
                timesNotInteracted++;

            if (timesNotInteracted >= 2) // Finished interacting
            {
                Interacted = false;
                break;
            }
            yield return interactor.SearchCooldown;
            OnInteract(interactor);
        }

        OnStopInteract(interactor);
    }
}