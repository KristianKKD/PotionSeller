using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : MonoBehaviour {

    public Step ingredient;
    public TMP_Text text;

    int quantityHeld = 10;


    private void Awake() {
        if (ingredient != null)
            text.text = ingredient.name;
    }

    public void Interact() {
        if (quantityHeld <= 0)
            return;

        GameObject go = Instantiate(References.r.templateIngredientPrefab, References.r.itemSpawnParent);
        go.GetComponent<Ingredient>().ingredientStep = ingredient; //temp (ingredients should have their own prefabs each)
        References.r.p.Grab(go.GetComponent<Rigidbody>());
        quantityHeld--;
    }

}
