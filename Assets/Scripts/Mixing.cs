using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mixing : MonoBehaviour {

    public List<Potion> availableRecipes = new List<Potion>();

    [SerializeField]
    List<Step> currentRecipe = new List<Step>();

    public Potion currentOutput;

    private void Awake() {
        
    }

    public void AddToMix(GameObject add) {
        Ingredient i = add.GetComponent<Ingredient>();
        currentRecipe.Add(i.step);
        //update properties
        //change colour
        Debug.Log("Added " + i.step.name);

        Destroy(add);
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
        return output;
    }


    private void OnTriggerEnter(Collider other) {
        AddToMix(other.gameObject);
    }


}
