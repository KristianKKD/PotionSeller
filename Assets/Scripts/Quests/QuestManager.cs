using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    List<Quest> currentActive = new List<Quest>();
    public List<Quest> questPool = new List<Quest>();

    public Transform questSpawnPoint;

    public int targetQuestsAvailable = 1;

    private void Awake() {
        for (int i = 0; i < targetQuestsAvailable; i++) {
            int randInt = Random.Range(0, questPool.Count);
            Quest selectedQuest = questPool[randInt];
            currentActive.Add(selectedQuest);
            questPool.Remove(selectedQuest);
            GiveQuest(selectedQuest);
        }
    }

    void GiveQuest(Quest q) {
        Debug.Log(References.r.questPagePrefab.name);
        GameObject go = Instantiate(References.r.questPagePrefab, References.r.itemSpawnParent);
        go.transform.position = questSpawnPoint.position;
        go.GetComponent<QuestPage>().LoadQuest(q);
    }

    public void CompletedQuest(Quest q) {
        currentActive.Remove(q);
        //give rewards
    }

}
