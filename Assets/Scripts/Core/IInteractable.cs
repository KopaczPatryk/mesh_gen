using Assets.Scripts.Core.Interaction;
using UnityEngine;

namespace Assets.Scripts.Core
{
    internal interface IInteractable
    {
        void Interact(RaycastHit hitInfo, InteractionType interaction);
    }
}