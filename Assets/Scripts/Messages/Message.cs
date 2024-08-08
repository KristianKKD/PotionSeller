using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PromptType {
    Prompt,
    Next,
    Cauldron,
}

[CreateAssetMenu(fileName = "Message", menuName = "Message", order = 0)]
public class Message : ScriptableObject {

    public string title;
    public string text;
    public float size = 1.3f;
    public Message prompt;
    public PromptType nextPromptType;

}
