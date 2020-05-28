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
    public Text selectedStageInfoText;

    private List<StageData> clearStageDataList;
    private int selectedStageNum = 1;

    void Start() {
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

                var showRightList = new List<Transform>();
                SetSelectedStage(selectedStageNum);
                showRightList.Add(selectedStageInfoText.transform);
                UIAnimationManager.ShowRight(showRightList);

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

                var hideRightList = new List<Transform>();
                hideRightList.Add(selectedStageInfoText.transform);
                UIAnimationManager.HideRight(hideRightList);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(playButton.transform);
                fadeOutList.Add(backButtonInStageSelect.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();

        playButton.onClick.AsObservable()
            .Do(_ => {
                PlayGame();

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(titleCanvas.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();
    }

    private void PlayGame() {
        gameManager.GameStart(selectedStageNum);
    }

    private void GetSaveData() {
        clearStageDataList = SaveData.GetClassList(SaveDataKey.clearStageDataList, SaveDataDefaultValue.clearStageDataList);
        SetSelectedStage(clearStageDataList.Count + 1);
    }

    private void SetSelectedStage(int stageNum) {
        if (stageNum > gameManager.stagePrefabList.Count) stageNum = gameManager.stagePrefabList.Count;
        selectedStageNum = stageNum;

        selectedStageInfoText.text = "LEVEL " + stageNum;
    }

    private void SetStageSelectScroll() {
        int allStageNum = gameManager.stagePrefabList.Count;
        for (int i = 0; i < allStageNum; i++) {
            GameObject stageSelectScrollItem = (GameObject)Instantiate(stageSelectScrollItemPrefab);
            stageSelectScrollItem.transform.SetParent(stageSelectScrollContent.transform);
            stageSelectScrollItem.transform.localScale = new Vector3(1, 1, 1);

            bool isCleared = i <= clearStageDataList.Count;
            var scrollItem = stageSelectScrollItem.GetComponent<StageSelectScrollItemManager>();
            var stageNum = i + 1;

            scrollItem.SetButtonInteractive(isCleared);
            scrollItem.SetText("LEVEL " + stageNum.ToString());
            scrollItem.ShowLockIcon(!isCleared);
            scrollItem.SetOnClickAction(() => SetSelectedStage(stageNum));
        }
    }

}
