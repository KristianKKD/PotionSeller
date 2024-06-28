using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mixing : MonoBehaviour {

    [SerializeField]
    List<Step> currentRecipeDisplay = new List<Step>();
    [SerializeField]
    List<Property> recipePropertiesDisplay = new List<Property>();
    
    public Potion currentOutput;

    public void AddToMix(Step addedStep) {
        currentOutput.AddStep(addedStep);

        recipePropertiesDisplay = currentOutput.currentProperties;
        currentRecipeDisplay = currentOutput.currentSteps;

        //change colour
        Debug.Log("Added " + addedStep.name);
    }

    public void OutputMix(Potion emptyPotion) { 
        emptyPotion.currentProperties = recipePropertiesDisplay;
        emptyPotion.currentSteps = currentRecipeDisplay;
        emptyPotion.gameObject.name = "Custom Potion";

        Debug.Log("Potion applied!");
        Debug.Log(emptyPotion.currentProperties[0].name);

        //change colour
        recipePropertiesDisplay.Clear();
        currentRecipeDisplay.Clear();
        currentOutput = null;
    }

    private void OnTriggerEnter(Collider other) {
        Ingredient i = other.GetComponent<Ingredient>();
        Potion prefab = other.GetComponent<Potion>();

        if (i != null) {
            AddToMix(i.ingredientStep);
            Destroy(other.gameObject); //destroy ingredients upon use
        } else if (prefab != null && prefab.IsEmpty() && currentOutput.currentSteps.Count > 0)
            OutputMix(prefab);

    }
}
