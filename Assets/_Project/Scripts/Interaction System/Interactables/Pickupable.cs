using UnityEngine;

public class Pickupable : Interactable
{
    public bool IsPicked { get; private set; }

    protected override void OnStartInteract(Interactor interactor, object[] par)
    {
        base.OnStartInteract(interactor, par);

        if (IsPicked)
            Drop();
        else
        {
            Vector2 newPosition = (Vector2)par[0];
            Quaternion quaternion = (Quaternion)par[1];
            Transform newParent = (Transform)par[2];

            Pickup(newPosition, quaternion, newParent);
        }
    }

    private void Drop()
    {
        if (!IsPicked)
            return;

        Debug.Log("DROP");
        IsPicked = false;
        transform.SetParent(null);
    }

    private void Pickup(Vector2 newPosition, Quaternion quaternion, Transform newParent = null)
    {
        if (IsPicked)
            return;

        Debug.Log("PICKUP");
        IsPicked = true;
        transform.position = newPosition;
        transform.rotation = quaternion;
        transform.SetParent(newParent);
    }
}
