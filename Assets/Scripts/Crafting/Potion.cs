using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Crafting/Potion", order = 4)]
public class Potion : ScriptableObject {
    public List<Step> steps = new List<Step>();
}
