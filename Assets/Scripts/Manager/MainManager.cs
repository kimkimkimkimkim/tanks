using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class MainManager : MonoBehaviour {
    public Button playButton;
    public Button optionButton;
    public Button statsButton;
    public Button infoButton;

    void Start() {
        playButton.onClick.AsObservable()
            .Do(_ => {
                var list = new List<Transform>();
                list.Add(playButton.transform);
                list.Add(optionButton.transform);
                list.Add(statsButton.transform);
                list.Add(infoButton.transform);
                HideLeft(list);
            })
            .Subscribe();

        optionButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();

        statsButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();

        infoButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();
    }

    private void HideLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float distance = 1218f;
            float time = 1.5f;
            transform.DOLocalMoveX(x - distance, time)
                .OnComplete(() => transform.gameObject.SetActive(false))
                .SetEase(Ease.OutCubic);
        }
    }

}
