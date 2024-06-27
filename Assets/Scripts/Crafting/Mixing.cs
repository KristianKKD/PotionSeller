using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mixing : MonoBehaviour {

    public List<Potion> availableRecipes = new List<Potion>();

    [SerializeField]
    List<Step> currentRecipe = new List<Step>();

    [SerializeField]
    List<Property> recipeProperties = new List<Property>();

    public Potion currentOutput;

    public void AddToMix(GameObject add) {
        Step s = add.GetComponent<Ingredient>().ingredientStep;
        currentRecipe.Add(s);

        for (int i = 0; i < s.propertiesApplied.Count; i++)
            recipeProperties.Add(s.propertiesApplied[i]);

        //update properties
        //change colour
        Debug.Log("Added " + s.name);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            Potion output = OutputMix();
            Debug.Log("Outputted: " + output.name);
        }
    }

    public Potion OutputMix() { //find a match if there is one
        Potion output = References.r.customPotion;

        for (int i = 0; i < availableRecipes.Count; i++) {

            bool match = false;
            for (int j = 0; j < currentRecipe.Count; j++) { //compare every step of every recipe to see if they are the same
                if (availableRecipes[i].steps[j] != currentRecipe[j]) { //if the step is different, this is not the same recipe
                    match = false;
                    break;
                }
                match = true; //this recipe is the same so far
            }

            if (match)
                output = availableRecipes[i];
        }

        currentOutput = References.r.basePotion;
        recipeProperties.Clear();
        currentRecipe.Clear();
        return output;
    }

    private void OnTriggerEnter(Collider other) {
        AddToMix(other.gameObject);
        Destroy(other.gameObject);
    }
}
