using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using TMPro;

public class ReadRecipe : MonoBehaviour {

    public Page myPage;

    public int xDim = 5;
    public int yDim = 5;

    public bool tracking = false;

    public Potion savedPotion;

    private void Awake() {
        savedPotion = GetComponent<Potion>();
        myPage.Spawn(xDim, yDim);
    }

    public void UpdateUserPotion(Potion pot) {
        if (tracking)
            OutputRecipe(pot);
    }

    public void OutputRecipe(Potion pot) {
        Read(pot, 0, 0, 0);
    }

    public void ClearRecipe() {
        Debug.Log("Clearing user tracking!");
        foreach (Transform child in myPage.transform) {
            PageCell cell = child.GetComponent<PageCell>();
            cell.text.text = "";
            cell.forwardArrow.SetActive(false);
            cell.loopArrow.SetActive(false);
            cell.loopText.text = "";
            cell.dotdot.SetActive(false);
            cell.continueArrow.SetActive(false);
        }
    }

    void Read(Potion pot, int potStepStartIndex, int currentX, int currentY) { //if there are > 1 conditions, the branches will smash
        int sameStep = 1;
        Step lastStep = null;
        PageCell lastCell = null;
        for (int stepIndex = potStepStartIndex; stepIndex < pot.recommendedSteps.Count; stepIndex++) {
            Step s = pot.currentSteps[stepIndex];
            if (lastStep != null && lastStep == s)
                sameStep += 1;
            else
                sameStep = 1;

            lastStep = s;

            bool wasContinued = false;
            if (currentX > xDim && currentY > yDim) {
                Debug.Log("Maxed out potion");
                lastCell.dotdot.SetActive(true);
                return;
            } else if (currentY + 1 > yDim) {
                currentX += 1;
                currentY = 0;
                wasContinued = true;
            }
            
            if (sameStep > 1) {
                if (stepIndex + 1 == pot.recommendedSteps.Count) //last cell
                    lastCell.forwardArrow.SetActive(false);

                lastCell.loopArrow.SetActive(true);
                lastCell.loopText.gameObject.SetActive(true);
                lastCell.loopText.text = "x" + sameStep.ToString();
                continue;
            }
            
            PageCell cell = myPage.transform.GetChild(currentX + currentY++ * xDim).GetComponent<PageCell>();
            cell.text.text = s.text;

            if (stepIndex + 1 < pot.recommendedSteps.Count && currentY < yDim)
                cell.forwardArrow.SetActive(true);
            if (wasContinued)
                cell.continueArrow.SetActive(true);
            

            lastCell = cell;

            if (s.type == StepType.Condition) {
                Read(pot, stepIndex + 1, currentX, currentY); //left side of the condition

                int leftTerminator = NextTerminator(pot, stepIndex);

                Read(pot, leftTerminator + 1, currentX + 1, currentY); //right side of the condition

                stepIndex = NextTerminator(pot, leftTerminator + 1);
            }

            if (s.type == StepType.Terminator)
                return;
        }

        savedPotion.Copy(pot); 
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
