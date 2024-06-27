using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StepType {
    Terminator,
    Statement,
    Condition,
}

[CreateAssetMenu(fileName = "Step", menuName = "Crafting/Step", order = 3)]
public class Step : ScriptableObject {
    public List<Property> propertiesApplied = new List<Property>();
    public StepType type = StepType.Statement;
    public string text;
}
