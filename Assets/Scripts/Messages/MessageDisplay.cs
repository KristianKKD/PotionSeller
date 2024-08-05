using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplay : MonoBehaviour {

    public void NextButton() {
        References.r.mm.NextMessage();
    }

    public void BackButton() {
        References.r.mm.PrevMessage();
    }

}
