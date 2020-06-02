using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingJoystick : Joystick {

    public GameManager gameManager;
    [HideInInspector] public PlayerTankShooting tankShooting;

    private Vector3 iniPos;

    protected override void Start() {
        background.SetAnchorWithKeepingPosition(0, 0);
        iniPos = background.position;
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        tankShooting.changeAimImageEnabled(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        background.position = iniPos;
        tankShooting.changeAimImageEnabled(false);
        tankShooting.Fire();
        base.OnPointerUp(eventData);
    }
}
