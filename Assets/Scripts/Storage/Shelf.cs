using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour {

    public Step ingredient;

    int quantityHeld = 10;

    References r;
    Pickup p;
    GameObject ingredientPrefab;

    private void Awake() {
        r = FindObjectOfType<References>();
        ingredientPrefab = r.templateIngredientPrefab.gameObject;
    }

    public void Interact() {
        if (quantityHeld <= 0)
            return;
        if (p == null)
            p = r.p;

        GameObject go = Instantiate(ingredientPrefab);
        go.GetComponent<Ingredient>().ingredientStep = ingredient;
        p.Grab(go.GetComponent<Rigidbody>());
        quantityHeld--;
    }

}
