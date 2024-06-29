using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public StepInteraction interaction;

    public Color colourMix;

    public bool Possible(List<Property> currentProperties) { //can this step be applied
        if (interaction != null)
            return interaction.ActivateInteraction(currentProperties, null) != null;
        return true;
    }

    public List<Property> Activate(List<Property> currentProperties) { //add the step to the mix
        List<Property> newProperties = propertiesApplied;
        if (interaction != null)
            newProperties = interaction.ActivateInteraction(currentProperties, propertiesApplied);

        currentProperties.AddRange(newProperties);

        return currentProperties;
    }
}
