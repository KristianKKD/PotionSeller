using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    List<Quest> currentActive = new List<Quest>();
    public List<Quest> questPool = new List<Quest>();

    public Transform questSpawnPoint;

    public int targetQuestsAvailable = 1;

    public List<GameObject> deliveredItems = new List<GameObject>();
    public List<GameObject> deliveredQuests = new List<GameObject>();

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
        GameObject go = Instantiate(References.r.questPagePrefab, References.r.itemSpawnParent);
        go.transform.position = questSpawnPoint.position;
        go.GetComponent<QuestPage>().LoadQuest(q);
    }

    public void AddDelivery(GameObject go) {
        QuestPage q = go.GetComponent<QuestPage>();
        if (q)
            deliveredQuests.Add(go);
        else
            deliveredItems.Add(go);

        Debug.Log("Added " + go.name);

        TryCompleteQuest();
    }

    public void RemoveDelivery(GameObject go) {
        deliveredItems.Add(go);
    }

    public void TryCompleteQuest() {
        List<GameObject> questsToRemove = new List<GameObject>();
        foreach (GameObject qGo in deliveredQuests) {
            Quest q = qGo.GetComponent<QuestPage>().q;
            List<GameObject> foundItems = new List<GameObject>();

            for (int requiredIndex = 0; requiredIndex < q.deliverables.Count; requiredIndex++) { //find a relevant quest page
                Potion requiredPotion = q.deliverables[requiredIndex];

                bool notMatch = false;
                foreach (GameObject g in deliveredItems) {
                    Potion deliveredPot = g.GetComponent<Potion>();
                    if (deliveredPot) {
                        for (int propIndex = 0; propIndex < requiredPotion.necessaryProperties.Count && !notMatch; propIndex++) { //check for all the desired properties
                            if (!deliveredPot.currentProperties.Contains(requiredPotion.necessaryProperties[propIndex])) {
                                notMatch = true;
                                Debug.Log("Missing " + requiredPotion.necessaryProperties[propIndex]);
                            }
                        }
                        for (int propIndex = 0; propIndex < requiredPotion.unwantedProperties.Count && !notMatch; propIndex++) { //check for bad properties
                            if (deliveredPot.currentProperties.Contains(requiredPotion.unwantedProperties[propIndex])) {
                                notMatch = true;
                                Debug.Log("Should not have " + requiredPotion.unwantedProperties[propIndex]);
                            }
                        }
                    }

                    if (!notMatch) { //found
                        foundItems.Add(g);
                        Debug.Log("Found " + g);
                        break;
                    }
                }
            }

            if (foundItems.Count == q.deliverables.Count) {
                CompleteQuest(q);
                for (int i = 0; i < foundItems.Count; i++)
                    Destroy(foundItems[foundItems.Count - 1]);
                questsToRemove.Add(qGo);
            } else
                Debug.Log("Missing " + (q.deliverables.Count - foundItems.Count).ToString());
        }

        for (int i = 0; i < questsToRemove.Count; i++)
            deliveredQuests.Remove(deliveredQuests[i]);
    }

    void CompleteQuest(Quest q) {
        Debug.Log("Completed " + q.title);
        currentActive.Remove(q);
        //give player rewards
    }
}
