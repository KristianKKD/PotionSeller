using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pickup : MonoBehaviour {

    GameObject heldObject;
    public float vecStartDist = 1;
    public float rotationSpeed = 100;
    public float followSpeed = 20;

    Vector3 previousPosition;
    Vector3 heldVelocity;

    public bool rotating = false;

    public Transform vecStartPos;

    public XRRayInteractor right;

    void Update() {
        if (right != null && right.gameObject.activeInHierarchy) {
            if (right.TryGetCurrent3DRaycastHit(out RaycastHit hit)) {
                Debug.Log("Ray Interactor is hitting: " + hit.collider.gameObject.name);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if (Physics.Raycast(vecStartPos.transform.position, vecStartPos.transform.TransformDirection(Vector3.forward) * vecStartDist, out hit)) {
                GameObject go = hit.collider.gameObject;
                Rigidbody rb = go.GetComponent<Rigidbody>();
                Shelf s = go.GetComponent<Shelf>();
                if (rb)
                    Grab(rb);
                else if (s) {
                    s.Interact();
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
            Drop();

        if (heldObject) {
            TrackMomentum();

            if (Input.GetMouseButton(0)) {
                RotateHeldObject();
                rotating = true;
            } else if (Input.GetMouseButtonUp(0))
                rotating = false;

        }
    }
    void RotateHeldObject() {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotateY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        heldObject.transform.Rotate(Vector3.up, rotateX, Space.World);
        heldObject.transform.Rotate(Vector3.right, rotateY, Space.World);
    }

    void TrackMomentum() {
        Vector3 targetPosition = vecStartPos.transform.position + vecStartPos.transform.TransformDirection(Vector3.forward);
        heldVelocity = (targetPosition - previousPosition) * followSpeed * Time.deltaTime;
        heldObject.transform.position += heldVelocity;
        previousPosition = heldObject.transform.position;
    }

    void Drop() {
        if (heldObject == null)
            return;

        ToggleCollision(true);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        heldObject.GetComponent<Collider>().enabled = true;
        rb.isKinematic = false;
        rb.velocity += heldVelocity * followSpeed;

        GolemBase golem = heldObject.GetComponent<GolemBase>();
        if (golem != null)
            golem.Dropped();

        heldObject = null;
    }

    public void Grab(Rigidbody objRB) {
        heldObject = objRB.gameObject;
        objRB.isKinematic = true;
        objRB.gameObject.GetComponent<Collider>().enabled = false;

        ToggleCollision(false);

        if (References.r.qm.deliveredItems.Contains(heldObject))
            References.r.qm.RemoveDelivery(heldObject);

        Ingredient i = heldObject.GetComponent<Ingredient>();
        if (i != null)
            References.r.id.UpdateDisplay(i.ingredientStep);
        else {
            GolemBase golem = heldObject.GetComponent<GolemBase>();
            if (golem != null)
                golem.PickedUp();
        }
    }

    void ToggleCollision(bool toggle) {
        for (int i = 0; i < heldObject.transform.childCount; i++) {
            Collider col = heldObject.transform.GetChild(i).GetComponent<Collider>();
            if (col != null)
                col.enabled = toggle;
        }
    }
}
