using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

    public class MessageManager : MonoBehaviour {

    public List<Message> messages = new List<Message>();
    public int messageIndex = 0;

    public TMP_Text text;
    public TMP_Text title;

    public GameObject forwardButton;
    public GameObject backButton;

    public GameObject tabPrefab;
    public GameObject tabSpawn;
    List<GameObject> tabs = new List<GameObject>();

    public TMP_Text prompt;

    public PromptType currentPromptType = PromptType.Next;

    bool completedIngredientPrompt = false;

    private void Awake() {
        DisplayMessage(0);
    }

    public void AddedIngredient(Step s) {
        if (currentPromptType == PromptType.Cauldron) {
            completedIngredientPrompt = true;
            prompt.text = messages[messageIndex].prompt.text; //secondary text in prompt
            forwardButton.SetActive(true);
        }
    }

    public void DisplayMessage(int index) {
        forwardButton.SetActive(false);
        backButton.SetActive(false);

        prompt.text = "";
        currentPromptType = PromptType.Prompt;

        Message m = messages[index];
        text.text = m.text;
        title.text = m.title;
        text.fontSize = m.size;

        if (m.prompt != null) {
            prompt.text = m.prompt.title;
            prompt.fontSize = m.prompt.size;
            currentPromptType = m.nextPromptType;

            if (currentPromptType == PromptType.Cauldron && completedIngredientPrompt) { //already completed the mini-quest
                currentPromptType = PromptType.Next;
                prompt.text = m.prompt.text; //secondary text in prompt
            }
        }

        if (index >= tabs.Count)
            SpawnTab(index);

        if (currentPromptType == PromptType.Next) {
            if (index + 1 <= messages.Count)
                forwardButton.SetActive(true);
        }

        if (index - 1 >= 0)
            backButton.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(null);
    }

    void SpawnTab(int index) {
        GameObject t = Instantiate(tabPrefab, tabSpawn.transform);
        tabs.Add(t);
        t.GetComponent<MessageTab>().Spawn(index, messages[index].title);
    }

    public void PressedTab(int index) {
        DisplayMessage(index);
        messageIndex = index;
    }

    public void NextMessage() {
        DisplayMessage(++messageIndex);
    }

    public void PrevMessage() {
        DisplayMessage(--messageIndex);
    }

}
