using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGrab : MonoBehaviour {

    public void Grab() {
        References.r.p.Grab(this.GetComponent<Rigidbody>());
    }

    public void Drop() {
        References.r.p.Drop();
    }

    public void InteractShelf(SelectEnterEventArgs args) {
        GetComponent<Shelf>().Interact(true, args);
    }

}
