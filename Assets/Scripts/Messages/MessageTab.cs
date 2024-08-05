using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageTab : MonoBehaviour {

    public TMP_Text text;
    int index = 0;

    public void Spawn(int myIndex, string newText) {
        index = myIndex;
        name = newText;
        text.text = newText;
    }

    public void Pressed() {
        References.r.mm.PressedTab(index);
    }

}
