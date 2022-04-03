using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pulls on the elevator cable.
public class ElevatorCable : MonoBehaviour
{
    // the elevator this applies to.
    public Elevator elevator;

    // the amount of pulls done everytime.
    public int pulls = 1;

    // Start is called before the first frame update
    void Start()
    {
        // grabs the component.
        if (elevator == null)
            elevator = GetComponentInParent<Elevator>();
    }

    // pulls the cable.
    private void OnMouseDown()
    {
        PullCable();
    }

    public void PullCable(int times)
    {
        // saves new value.
        pulls = times;

        // elevator is set.
        if (elevator != null)
        {
            // the elevator can't be interacted with.
            if (!elevator.interactable)
                return;

            // pulls on the cable.
            elevator.PullCable();
        }
    }

    // pulls on the cable.

    public void PullCable()
    {
        PullCable(pulls);
    }
}
