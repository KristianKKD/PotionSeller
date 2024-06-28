using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

public class Recipe {
    public List<Node> nodes = new List<Node>();
}

public class ReadRecipe : MonoBehaviour {

    public GameObject page;

    public int dimensions = 20;

    public bool a = false;
    public Potion testPotion;

    private void Awake() {
        //OutputRecipe(testPotion);
    }

    private void Update() {
        if (a) {
            a = false;
            OutputRecipe(testPotion);
        }
    }

    public void OutputRecipe(Potion pot) {
        Read(pot, 0, 0, 0);
    }

    void Read(Potion pot, int potStepStartIndex, int currentX, int currentY) { //if there are > 1 conditions, the branches will smash
        for (int stepIndex = potStepStartIndex; stepIndex < pot.recommendedSteps.Count; stepIndex++) {
            Step s = pot.recommendedSteps[stepIndex];

            GameObject pageSlot = page.transform.GetChild(currentX + currentY++ * dimensions).gameObject;
            Debug.Log(currentX.ToString() + ", " + currentY.ToString() + ", " + pageSlot.name + ", " + s.text);
            pageSlot.GetComponent<TMP_Text>().text = s.text;

            if (s.type == StepType.Condition) {
                Read(pot, stepIndex + 1, currentX, currentY); //left side of the condition

                int leftTerminator = NextTerminator(pot, stepIndex);

                Read(pot, leftTerminator + 1, currentX + 1, currentY); //right side of the condition

                stepIndex = NextTerminator(pot, leftTerminator + 1);
            }

            if (s.type == StepType.Terminator)
                return;
        }
    }

    int NextTerminator(Potion pot, int startIndex) {
        //find the seperation point between the condition
        for (int secondaryIndex = startIndex + 1; secondaryIndex < pot.recommendedSteps.Count; secondaryIndex++) {
            if (pot.recommendedSteps[secondaryIndex].type == StepType.Terminator)
                return secondaryIndex;
        }

        return pot.recommendedSteps.Count - 1;
    }
}
