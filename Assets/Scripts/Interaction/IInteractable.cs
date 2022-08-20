using UnityEngine;

namespace Interaction {
    internal interface IInteractable {
        void Interact(RaycastHit hitInfo, InteractionType interaction);
    }
}