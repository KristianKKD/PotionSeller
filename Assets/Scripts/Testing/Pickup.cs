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
                if (rb) {
                    heldObject = hit.collider.gameObject;
                    rb.isKinematic = true;
                }

            }
        }

        if (Input.GetMouseButtonUp(0) && heldObject) {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
        }

        if (heldObject)
            heldObject.transform.position = transform.position + transform.TransformDirection(Vector3.forward);
    }
}
