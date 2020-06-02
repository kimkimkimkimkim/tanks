﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : Joystick {

    private float inactiveAlpha = 1f;
    private float activeAlpha = 1f;
    private float screenWidth;
    private Vector3 iniPos;

    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;

    protected override void Start() {
        MoveThreshold = moveThreshold;
        iniPos = background.position;
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        background.position = iniPos;
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam) {
        if (magnitude > moveThreshold) {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}
