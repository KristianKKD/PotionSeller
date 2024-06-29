using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPage : MonoBehaviour {

    public Quest q;

    public TMP_Text title;
    public TMP_Text conditions;
    public TMP_Text description;
    public TMP_Text rewards;
    public TMP_Text payout;

    public void LoadQuest(Quest newQuest) {
        q = newQuest;
        title.text = q.title;
        description.text = q.description;

        conditions.text = "";
        for (int i = 0; i < q.deliverables.Count; i++)
            conditions.text += "Deliver Potion of " + q.deliverables[i].name + "\n";

        rewards.text = "";
        for (int i = 0; i < q.rewards.Count; i++)
            rewards.text += q.rewards[i].name + "\n";

        payout.text = "$" + q.payout.ToString();
    }

}
