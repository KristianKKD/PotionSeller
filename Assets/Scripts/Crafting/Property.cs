using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Property", menuName = "Crafting/Property", order = 0)]
public class Property : ScriptableObject {
    public List<Property> counters = new List<Property>();

    public List<float> chances = new List<float>();
    public List<Property> chanceApplied = new List<Property>();
}
