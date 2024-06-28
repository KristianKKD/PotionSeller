using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    GameObject heldObject;
    public float vecStartDist;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * vecStartDist, out hit)) {
                GameObject go = hit.collider.gameObject;
                Rigidbody rb = go.GetComponent<Rigidbody>();
                Shelf s = go.GetComponent<Shelf>();
                if (rb)
                    Grab(rb);
                else if (s)
                    s.Interact();
            }
        }

        if (Input.GetMouseButtonUp(0))
            Drop();

        if (heldObject)
            heldObject.transform.position = transform.position + transform.TransformDirection(Vector3.forward);
    }

    void Drop() {
        if (heldObject == null)
            return;

        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }

    public void Grab(Rigidbody objRB) {
        heldObject = objRB.gameObject;
        objRB.isKinematic = true;
        objRB.gameObject.GetComponent<Collider>().enabled = false;
    }
}
