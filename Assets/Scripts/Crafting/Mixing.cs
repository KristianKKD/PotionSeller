using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mixing : MonoBehaviour {

    [SerializeField]
    List<Step> currentRecipe = new List<Step>();
    [SerializeField]
    List<Property> currentRecipeProperties = new List<Property>();
    
    public Potion currentOutput;

    public GameObject potionMixIndicator;

    public void AddToMix(Step addedStep) {
        currentOutput.AddStep(addedStep);

        currentRecipeProperties = currentOutput.currentProperties;
        currentRecipe = currentOutput.currentSteps;

        Color c = Color.black;
        for (int i = 0; i < currentRecipe.Count; i++)
            c += currentRecipe[i].colourMix;
        c /= currentRecipe.Count; //average colour

        potionMixIndicator.SetActive(true);
        potionMixIndicator.GetComponent<Renderer>().material.color = c;

        Debug.Log("Added " + addedStep.name);
    }

    public void OutputMix(GameObject emptyPotion) {
        Potion p = emptyPotion.GetComponent<Potion>();
        p.Copy(currentOutput);
        emptyPotion.GetComponent<Renderer>().material.color = potionMixIndicator.GetComponent<Renderer>().material.color;
        emptyPotion.name = "Custom Potion";

        Debug.Log("Potion applied!");

        potionMixIndicator.SetActive(false);
        currentRecipeProperties.Clear();
        currentRecipe.Clear();
        currentOutput.currentSteps.Clear();
        currentOutput.currentProperties.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        Ingredient i = other.GetComponent<Ingredient>();
        Potion prefab = other.GetComponent<Potion>();

        if (i != null) {
            AddToMix(i.ingredientStep);
            Destroy(other.gameObject); //destroy ingredients upon use
        } else if (prefab && prefab.IsEmpty() && currentRecipe.Count > 0)
            OutputMix(other.gameObject);
    }
}
