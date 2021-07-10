using UnityEngine;

[RequireComponent(typeof(Interactor))]
public class CharacterInteractor : MonoBehaviour
{
    private Interactor _interactor;
    public Pickupable Pickupable { get; private set; }

    private void Awake() => _interactor = GetComponent<Interactor>();

    private void Update()
    {
        // Interaction
        bool startInteract = Input.GetKeyDown(KeyCode.E);

        if (startInteract)
        {
            Pickupable oldPickupable = Pickupable;
            if (Pickupable)
                DropPickupable();

            Interactable interactable = _interactor.GetSelectedInteractable(oldPickupable);
            if (interactable is Pickupable pickupable)
            {
                if (Pickupable == null)
                {
                    Pickupable = pickupable;
                    Pickupable.Interact(_interactor, InteractionType.Start, (Vector2)(transform.position + transform.right),
                        transform.rotation, transform);
                }
            }
        }
    }

    private void DropPickupable()
    {
        if (Pickupable == null)
            return;

        Pickupable.Interact(_interactor, InteractionType.Start);
        Pickupable = null;
    }
}
