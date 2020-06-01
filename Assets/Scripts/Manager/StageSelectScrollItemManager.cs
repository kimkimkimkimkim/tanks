using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StageSelectScrollItemManager : MonoBehaviour {
    public Button button;
    public Text text;
    public Image lockIcon;
    public Image background;
    public Color selectedColor;
    public Color unSelectedColor;

    public void SetButtonInteractive(bool isInteractive) {
        button.interactable = isInteractive;
    }

    public void SetText(string str) {
        text.text = str;
    }

    public void ShowLockIcon(bool isShow) {
        lockIcon.gameObject.SetActive(isShow);
    }

    public void SetBackgroundColor(bool isSelected) {
        background.color = (isSelected) ? selectedColor : unSelectedColor;
    }

    public void SetOnClickAction(Action collbackAction) {
        button.onClick.AsObservable()
                .Do(_ => collbackAction())
                .Subscribe();
    }
}
