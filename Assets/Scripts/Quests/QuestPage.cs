using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPage : MonoBehaviour {

    public Quest q;

    public TMP_Text title;
    public TMP_Text required;
    public TMP_Text avoid;
    public TMP_Text description;
    public TMP_Text rewards;
    public TMP_Text payout;

    public Image indicator;

    public ReadRecipe backRecipe;

    public void LoadQuest(Quest newQuest) {
        q = newQuest;
        title.text = q.title;
        description.text = q.description;
        q.deliverables[0].currentSteps = q.deliverables[0].recommendedSteps;
        q.deliverables[0].UpdateColour();
        indicator.color = q.deliverables[0].indicatorColor;

        required.text = "";
        for (int i = 0; i < q.deliverables.Count; i++)
            required.text += IngredientDisplay.CollectPropertyText(q.deliverables[i].necessaryProperties);

        avoid.text = "";
        for (int i = 0; i < q.deliverables.Count; i++)
            avoid.text += IngredientDisplay.CollectPropertyText(q.deliverables[i].unwantedProperties);

        rewards.text = "";
        for (int i = 0; i < q.rewards.Count; i++)
            rewards.text += q.rewards[i].name + "\n";

        payout.text = "$" + q.payout.ToString();

        backRecipe.OutputRecipe(q.deliverables[0]);
    }

}
