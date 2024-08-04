using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : MonoBehaviour {

    public Step ingredient;
    public TMP_Text text;
    public TMP_Text countText;

    public int quantityHeld = 10;


    private void Awake() {
        if (ingredient != null) {
            text.text = ingredient.name;
            countText.text = "x" + quantityHeld.ToString();
        }
    }

    public void Interact(bool isPlayer = true) {
        if (ingredient == null || quantityHeld <= 0)
            return;
        
        --quantityHeld;
        countText.text = "x" + quantityHeld.ToString();

        if (isPlayer) {
            GameObject go = Instantiate(References.r.templateIngredientPrefab, References.r.itemSpawnParent);
            go.transform.position = this.transform.position;
            go.GetComponent<Ingredient>().ingredientStep = ingredient; //temp (ingredients should have their own prefabs each)
            go.GetComponent<Renderer>().material.color = ingredient.colourMix;

            References.r.p.Grab(go.GetComponent<Rigidbody>());
            References.r.id.UpdateDisplay(ingredient);
        }
    }

}
