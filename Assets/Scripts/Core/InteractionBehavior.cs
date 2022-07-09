using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core.Interaction;
using UnityEngine;

namespace Assets.Scripts.Core {
    class InteractionBehavior : MonoBehaviour {
        void Update() {
            if (Input.GetMouseButton(0)) {
                RaycastHit hitInfo = new RaycastHit();
                bool hitRegistered = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 50);
                if (hitRegistered) {
                    var beh = hitInfo.collider.GetComponent<IInteractable>();
                    if (beh != null) {
                        beh.Interact(hitInfo, InteractionType.Destroy);
                    }
                }
            }
        }
    }
}
