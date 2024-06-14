using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Recipe {
    public List<Node> nodes = new List<Node>();
}

public class ReadRecipe : MonoBehaviour {

    int[,] idGrid;

    public void OutputRecipe(Potion pot) {
        int x = 5;
        int y = 5;
        Read(pot, 0, x, y);
    }

    void Read(Potion pot, int stepStartIndex, int x, int y) { //if there are > 1 conditions, the branches will smash
        for (int stepIndex = stepStartIndex; stepIndex < pot.steps.Count; stepIndex++) {
            Step s = pot.steps[stepIndex];

            idGrid[x, y++] = stepIndex; //save the node

            if (s.type == StepType.Condition) {
                //remember the condition position
                int currentPosX = x;
                int currentPosY = y;

                Read(pot, stepIndex + 1, currentPosX - 1, currentPosY); //left side of the condition

                //find the seperation point between the condition
                int nextTerminator = 0;
                for (int j = stepIndex + 1; j < pot.steps.Count && nextTerminator == 0; j++) {
                    if (pot.steps[j].type == StepType.Terminator)
                        nextTerminator = j;
                }

                Read(pot, nextTerminator + 1, currentPosX + 1, currentPosY); //right side of the condition
            }
        }
    }

}
