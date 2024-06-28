using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Potion : MonoBehaviour {
    public List<Property> necessaryProperties = new List<Property>();
    public List<Property> unwantedProperties = new List<Property>();
    public List<Step> recommendedSteps = new List<Step>();

    //when making
    public List<Step> currentSteps = new List<Step>();
    public List<Property> currentProperties = new List<Property>();

    public Potion(List<Property> necessary = null, List<Property> unwanted = null, List<Step> recSteps = null) {
        necessaryProperties = necessary;
        unwantedProperties = unwanted;
        recommendedSteps = recSteps;
    }

    public void AddStep(Step s) {
        currentSteps.Add(s);
        currentProperties = s.Activate(currentProperties);
    }

    public bool IsEmpty() {
        return currentSteps.Count == 0;
    }
}


