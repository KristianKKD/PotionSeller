using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        References.r.q.AddDelivery(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        References.r.q.RemoveDelivery(other.gameObject);
    }

}
