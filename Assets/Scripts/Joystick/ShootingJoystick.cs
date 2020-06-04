using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingJoystick : Joystick {

    public GameManager gameManager;
    [HideInInspector] public PlayerTankShooting tankShooting;

    private Vector3 iniPos;
    private bool isAuto;

    protected override void Start() {
        background.SetAnchorWithKeepingPosition(0, 0);
        iniPos = background.position;
        base.Start();
    }

    private void FixedUpdate() {
        if (Direction != Vector2.zero) {
            tankShooting.TurnTank(Direction);
        }
    }

    public override void OnPointerDown(PointerEventData eventData) {
        isAuto = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnDrag(PointerEventData eventData) {
        base.OnDrag(eventData);

        if (Direction == Vector2.zero) {
            tankShooting.ChangeAimImageEnabled(false);
        } else {
            isAuto = false;
            tankShooting.ChangeAimImageEnabled(true);
        }
    }

    public override void OnPointerUp(PointerEventData eventData) {
        background.position = iniPos;
        tankShooting.ChangeAimImageEnabled(false);
        if (Direction == Vector2.zero) {
            if (isAuto) AutoFire();
        } else {
            ManualFire();
        }

        base.OnPointerUp(eventData);
    }

    private void AutoFire() {
        tankShooting.Fire();
    }

    private void ManualFire() {
        tankShooting.Fire();
    }
}
