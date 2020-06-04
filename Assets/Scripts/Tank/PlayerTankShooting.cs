using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankShooting : TankShooting {

    public Image aimImage;

    public void TurnTank(Vector3 vector) {
        vector = new Vector3(vector.x, 0, vector.y);
        tankTurret.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
    }

    public void ChangeAimImageEnabled(bool enabled) {
        aimImage.enabled = enabled;
    }
}
