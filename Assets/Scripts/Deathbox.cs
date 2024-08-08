using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbox : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Potion>() != null && other.gameObject.GetComponent<Ingredient>() != null) { //bottle
            Destroy(other.gameObject);
            Instantiate(References.r.potionPrefab, References.r.respawnPotionParent);
            return;
        }
        other.gameObject.transform.position = Vector3.up;
    }

}
