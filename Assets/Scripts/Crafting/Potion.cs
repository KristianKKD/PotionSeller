using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {
    public List<Property> necessaryProperties = new List<Property>();
    public List<Property> unwantedProperties = new List<Property>();
    public List<Step> recommendedSteps = new List<Step>();

    //when making
    public List<Step> currentSteps = new List<Step>();
    public List<Property> currentProperties = new List<Property>();

    public GameObject contentsIndicator;
    public List<Step> savedRecipe;

    public Color indicatorColor = Color.black;

    public Potion(List<Property> necessary = null, List<Property> unwanted = null, List<Step> recSteps = null) {
        necessaryProperties = necessary;
        unwantedProperties = unwanted;
        recommendedSteps = recSteps;
    }

    public void AddStep(Step s, bool addRecommended = false) {
        currentSteps.Add(s);

        if (addRecommended)
            recommendedSteps.Add(s);

        currentProperties = s.Activate(currentProperties);

        UpdateColour();
    }

    public void UpdateColour() {
        Color c = Color.black;
        for (int i = 0; i < currentSteps.Count; i++)
            c += currentSteps[i].colourMix;
        c /= currentSteps.Count; //average colour
        c.a = 1;
        indicatorColor = c;

        if (contentsIndicator == null)
            return;

        contentsIndicator.SetActive(true);
        contentsIndicator.GetComponent<Renderer>().material.color = indicatorColor;
    }

    public bool IsEmpty() {
        return currentSteps.Count == 0;
    }

    public void Copy(Potion other) {
        currentProperties.Clear();
        currentSteps.Clear();
        for (int i = 0; i < other.currentProperties.Count; i++)
            currentProperties.Add(other.currentProperties[i]);
        for (int i = 0; i < other.currentSteps.Count; i++)
            currentSteps.Add(other.currentSteps[i]);

        UpdateColour();
    }

    public void Reset() {
        currentSteps.Clear();
        currentProperties.Clear();
        recommendedSteps.Clear();
    }
}


