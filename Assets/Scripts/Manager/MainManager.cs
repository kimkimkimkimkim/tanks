using System;
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
    public GameObject stageSelectScrollView;
    public GameObject stageSelectScrollContent;
    public GameObject stageSelectScrollItemPrefab;

    void Start() {
        playButton.onClick.AsObservable()
            .Do(_ => {
                var list = new List<Transform>();
                list.Add(playButton.transform);
                list.Add(optionButton.transform);
                list.Add(statsButton.transform);
                list.Add(infoButton.transform);
                HideLeft(list);

                var showList = new List<Transform>();
                SetStageSelectScroll();
                showList.Add(stageSelectScrollView.transform);
                ShowLeft(showList);
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
            float time = 1f;
            transform.DOLocalMoveX(x - distance, time)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => {
                    transform.gameObject.SetActive(false);
                });
        }
    }

    private void ShowLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            float distance = 1218f;
            float time = 1f;
            transform.localPosition = new Vector3(x - distance,y,z);
            transform.gameObject.SetActive(true);
            transform.DOLocalMoveX(x,time)
                .SetEase(Ease.InCubic)
                .OnComplete(() => {

                });
        }
    }

    private void SetStageSelectScroll(){
        int allStageNum = 33;
        for(int i=0;i<allStageNum;i++){
            GameObject stageSelectScrollItem = (GameObject)Instantiate(stageSelectScrollItemPrefab);
            stageSelectScrollItem.transform.SetParent(stageSelectScrollContent.transform);
            stageSelectScrollItem.transform.localScale = new Vector3(1,1,1);
        }
    }

}
