using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Crafting/Potion", order = 4)]
public class ScriptablePotion : ScriptableObject {
    //to compare to
    public List<Property> necessaryProperties = new List<Property>();
    public List<Step> recommendedSteps = new List<Step>();
    public List<Property> unwantedProperties = new List<Property>();

    //when making
    public List<Step> currentSteps = new List<Step>();
    public List<Property> currentProperties = new List<Property>();

    public void AddStep(Step s) {
        currentSteps.Add(s);
        currentProperties = s.Activate(currentProperties);
    }
}