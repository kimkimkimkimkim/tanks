using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankHealth : TankHealth {

    [HideInInspector] public GameObject healthImageContainer;
    [HideInInspector] public Color onHealthColor;
    [HideInInspector] public Color offHealthColor;

    public override void TakeDamage(int amount) {
        base.TakeDamage(amount);

        var index = 0;
        var totalDamage = maxHealth - currentHealth;
        foreach (Transform healthImage in healthImageContainer.transform) {
            healthImage.GetComponent<Image>().color = (index < totalDamage) ? offHealthColor : onHealthColor;
            index++;
        }
    }
}
