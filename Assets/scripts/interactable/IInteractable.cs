using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // for stuff like showing/hiding interaction popup overlays
    public void EnteredInteractionArea();
    public void ExitInteractionArea();
    // the actual trigger for when the player hits the button
    public void Interact();

    /// <summary>
    /// the pop up overlay (ie "Talk", "Investigate", "Read")
    /// </summary>
    public string InteractionText { get; }
}
