using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankShooting : TankShooting {

    public Image aimImage;
    [HideInInspector] public CpuTankManager[] cpuTankList;
    [HideInInspector] public Color tankColor;

    public void TurnTank(Vector3 vector) {
        vector = new Vector3(vector.x, 0, vector.y);
        tankTurret.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
    }

    public void ChangeAimImageEnabled(bool enabled) {
        aimImage.enabled = enabled;
    }

    public void AutoFire(){
        Debug.Log(cpuTankList.Length);
        float minDistance = float.MaxValue;
        Transform targetTransform = null;
        foreach(CpuTankManager cpu in cpuTankList){
            if(!cpu.instance.activeSelf)continue;
            Transform cpuTransform = cpu.instance.transform;
            float distance = (transform.position - cpuTransform.position).magnitude;
            if(distance <= minDistance){
                minDistance = distance;
                targetTransform = cpuTransform;
            }
        }

        if(targetTransform != null){
            Vector3 vector = targetTransform.position - transform.position;
            vector = new Vector3(vector.x, vector.z , 0);
            TurnTank(vector);
            Fire(tankColor);
        }
    }

    public void ManualFire(){
        Fire(tankColor);
    }
}
