using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour { //place on a panel with a grid layout group

    public GameObject cell;
    public int cellsWidth = 20;
    public int cellsHeight = 20;

    private void Awake() {
        for (int y = 0; y < cellsHeight; y++) {
            for (int x = 0; x < cellsWidth; x++) {
                GameObject go = Instantiate(cell, transform);
                go.name = (x + y * cellsHeight).ToString();
            }
        }
    }

}
