using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class MainManager : MonoBehaviour {
    public Button goToStageSelectButton;
    public Button optionButton;
    public Button statsButton;
    public Button infoButton;
    public GameObject stageSelectScrollView;
    public GameObject stageSelectScrollContent;
    public GameObject stageSelectScrollItemPrefab;
    public Button playButton;
    public Button backButtonInStageSelect;
    public GameManager gameManager;
    public GameObject titleCanvas;

    private List<StageData> clearStageDataList;

    void Start() {
        SetSaveData();
        GetSaveData();


        goToStageSelectButton.onClick.AsObservable()
            .Do(_ => {
                var list = new List<Transform>();
                list.Add(goToStageSelectButton.transform);
                list.Add(optionButton.transform);
                list.Add(statsButton.transform);
                list.Add(infoButton.transform);
                UIAnimationManager.HideLeft(list);

                var showList = new List<Transform>();
                SetStageSelectScroll();
                showList.Add(stageSelectScrollView.transform);
                UIAnimationManager.ShowLeft(showList);

                var fadeInList = new List<Transform>();
                fadeInList.Add(playButton.transform);
                fadeInList.Add(backButtonInStageSelect.transform);
                UIAnimationManager.FadeIn(fadeInList);
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

        backButtonInStageSelect.onClick.AsObservable()
            .Do(_ => {
                var showList = new List<Transform>();
                showList.Add(goToStageSelectButton.transform);
                showList.Add(optionButton.transform);
                showList.Add(statsButton.transform);
                showList.Add(infoButton.transform);
                UIAnimationManager.ShowLeft(showList);

                var hideList = new List<Transform>();
                hideList.Add(stageSelectScrollView.transform);
                UIAnimationManager.HideLeft(hideList);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(playButton.transform);
                fadeOutList.Add(backButtonInStageSelect.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();

        playButton.onClick.AsObservable()
            .Do(_ => {
                gameManager.GameStart(1);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(titleCanvas.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();
    }

    private void SetSaveData() {
        List<StageData> list = new List<StageData> {
            new StageData(1, 0)
        };
        SaveData.SetClassList(SaveDataKey.clearStageDataList, list);
    }

    private void GetSaveData() {
        clearStageDataList = SaveData.GetClassList(SaveDataKey.clearStageDataList, SaveDataDefaultValue.clearStageDataList);
        foreach (StageData stageData in clearStageDataList) {
            Debug.Log(stageData.number);
        }
    }

    private void SetStageSelectScroll() {
        int allStageNum = 33;
        for (int i = 0; i < allStageNum; i++) {
            GameObject stageSelectScrollItem = (GameObject)Instantiate(stageSelectScrollItemPrefab);
            stageSelectScrollItem.transform.SetParent(stageSelectScrollContent.transform);
            stageSelectScrollItem.transform.localScale = new Vector3(1, 1, 1);

            Button button = stageSelectScrollItem.GetComponent<Button>();
            Text text = stageSelectScrollItem.transform.GetChild(0).GetComponent<Text>();
            Image lockIcon = stageSelectScrollItem.transform.GetChild(1).GetComponent<Image>();

            button.interactable = (i == 0);
            text.text = "LEVEL " + (i + 1).ToString();
            lockIcon.gameObject.SetActive(i != 0);
        }
    }

}
