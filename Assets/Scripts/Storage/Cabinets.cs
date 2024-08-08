using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinets : MonoBehaviour {

    public List<Shelf> shelves = new List<Shelf>();

    public void AddShelf(Step st, int quantity) {
        foreach (Shelf shelf in shelves) {
            if (shelf.ingredient = st) {
                shelf.quantityHeld += quantity;
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
