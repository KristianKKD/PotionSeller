using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour {

    public static References r;

    public Potion basePotion;
    public Potion customPotion;

    private void Awake() {
        r = this;
    }

}
