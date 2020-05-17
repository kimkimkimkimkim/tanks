using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData {
    public int number;
    public int evaluation;

    public StageData() {

    }

    public StageData(int number, int evaluation) {
        this.number = number;
        this.evaluation = evaluation;
    }
}
