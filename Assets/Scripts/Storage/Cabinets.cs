using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinets : MonoBehaviour {

    List<Shelf> shelves;

    private void Awake() {
        shelves = new List<Shelf>(GetComponentsInChildren<Shelf>(true));
    }

    public void AddShelf(Step st, int quantity) {
        Debug.Log("Adding " + st.name + " shelf");
        foreach (Shelf shelf in shelves) {
            if (shelf.ingredient == st) {
                shelf.quantityHeld += quantity;
                break;
            } else if (shelf.ingredient == null) {
                shelf.ingredient = st;
                shelf.quantityHeld = quantity;
                shelf.gameObject.SetActive(true);
                shelf.name = st.name;
                break;
            }
        }
    }

}
