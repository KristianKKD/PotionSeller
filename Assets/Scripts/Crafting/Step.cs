using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public enum StepType {
    Terminator,
    Statement,
    Condition,
}

public abstract class StepInteraction {
    public abstract List<Property> ActivateInteraction(List<Property> recipeProperties, List<Property> appliedProperties);
}

[CreateAssetMenu(fileName = "Step", menuName = "Crafting/Step", order = 3)]
public class Step : ScriptableObject {
    public List<Property> propertiesApplied = new List<Property>();
    public StepType type = StepType.Statement;
    public string text;
    public string description;
    public StepInteraction interaction;

    public Color colourMix;

    public bool Possible(List<Property> currentProperties) { //can this step be applied
        if (interaction != null)
            return interaction.ActivateInteraction(currentProperties, null) != null;
        return true;
    }

    public List<Property> Activate(List<Property> currentProperties) { //add the step to the mix
        List<Property> newProperties = new List<Property>(propertiesApplied);
        
        //random chances to add
        List<Property> chanceProperties = new List<Property>(); //don't modify the list we are currently already working on
        for (int i = 0; i < newProperties.Count; i++) {
            Property appliedProperty = newProperties[i];

            for (int j = 0; j < appliedProperty.chanceApplied.Count; j++) {
                Property chanceProperty = appliedProperty.chanceApplied[j];
                float chance = appliedProperty.chances[j] * 100;

                int rand = Random.Range(0, 100);
                if (rand < chance)
                    chanceProperties.Add(chanceProperty);
            }
        }

        for (int i = 0; i < chanceProperties.Count; i++)
            newProperties.Add(chanceProperties[i]);

        //neutralize counters
        List<Property> neutralizedProperties = new List<Property>(); //the to be applied properties that were neutralized
        for (int i = 0; i < newProperties.Count; i++) { //for every newly applied property
            Property appliedProperty = newProperties[i];
            bool used = false;

            for (int j = 0; j < appliedProperty.counters.Count; j++) { //for every counter per property
                Property appliedCounter = appliedProperty.counters[j];
                if (currentProperties.Contains(appliedCounter)) { //if the current properties contain the counter property, remove it (x1)
                    used = true;
                    currentProperties.Remove(appliedCounter);
                }
            }

            if (used)
                neutralizedProperties.Add(appliedProperty);
        }

        //remove the used properties
        for (int i = 0; i < neutralizedProperties.Count; i++)
            newProperties.Remove(neutralizedProperties[i]);

        if (interaction != null) //specific interactions
            newProperties = interaction.ActivateInteraction(currentProperties, newProperties);

        currentProperties.AddRange(newProperties);

        return currentProperties;
    }
}
