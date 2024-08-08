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
        currentOutput.AddStep(addedStep, true);

        currentRecipeProperties = currentOutput.currentProperties;
        currentRecipe = currentOutput.currentSteps;

        References.r.pt.UpdateUserPotion(currentOutput);
        References.r.pp.UpdateDisplay();

        References.r.mm.AddedIngredient(addedStep); //player may be on the add ingredient quest

        Debug.Log("Added " + addedStep.name);
    }

    public void OutputMix(GameObject emptyPotion) {
        Potion p = emptyPotion.GetComponent<Potion>();
        p.Copy(currentOutput);
        emptyPotion.name = "Custom Potion";

        Debug.Log("Potion applied!");

        potionMixIndicator.SetActive(false);
        currentRecipeProperties.Clear();
        currentRecipe.Clear();

        currentOutput.Reset();
        References.r.pt.ClearRecipe();
    }

    private void OnTriggerEnter(Collider other) {
        Ingredient i = other.GetComponent<Ingredient>();
        Potion prefab = other.GetComponent<Potion>();

        if (i != null && i.ingredientStep != References.r.bottle) {
            AddToMix(i.ingredientStep);
            Destroy(other.gameObject); //destroy ingredients upon use
        } else if (prefab && prefab.IsEmpty() && currentRecipe.Count > 0)
            OutputMix(other.gameObject);
    }
}
