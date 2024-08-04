using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour { //place on a panel with a grid layout group

    public GameObject cell;

    public void Spawn(int xDim, int yDim) {
        for (int y = 0; y < yDim; y++) {
            for (int x = 0; x < xDim; x++) {
                GameObject go = Instantiate(cell, transform);
                go.name = (x + y * yDim).ToString();
            }
        }
    }

}
