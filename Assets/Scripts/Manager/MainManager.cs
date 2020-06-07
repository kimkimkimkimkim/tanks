using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public enum UIMode {
    Default,
    StageSelect,
}

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
    public Sprite backgroundSprite;
    public Image background;
    public Transform camera;
    public Vector3 cameraPosDefault;
    public Vector3 cameraPosStageSelect;
    public GameObject uiCanvas;
    public GameObject messageCanvas;

    private List<StageData> clearStageDataList;
    private int selectedStageNum = 1;

    void Start() {
        GetSaveData();
        background.sprite = null;

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

                SetUIMode(UIMode.StageSelect);
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
                SetUIMode(UIMode.Default);

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
                SetUIMode(UIMode.Default);
                PlayGame();

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(titleCanvas.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();
    }

    private void SetUIMode(UIMode uiMode){
        if(uiMode == UIMode.Default){
            background.sprite = null;
            camera.position = cameraPosDefault;
            uiCanvas.SetActive(true);
            messageCanvas.SetActive(true);
        }else if(uiMode == UIMode.StageSelect){
            background.sprite = backgroundSprite;
            camera.position = cameraPosStageSelect;
            uiCanvas.SetActive(false);
            messageCanvas.SetActive(false);
        }
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

        for (int i = 0; i < stageSelectScrollContent.transform.childCount; i++) {
            var scrollItem = stageSelectScrollContent.transform.GetChild(i).GetComponent<StageSelectScrollItemManager>();
            scrollItem.SetBackgroundColor((i + 1) == selectedStageNum);
        }

        gameManager.ResetGame();
        gameManager.SetStageForStageSelect(stageNum);
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
            scrollItem.SetBackgroundColor(selectedStageNum == stageNum);
            scrollItem.SetOnClickAction(() => {
              SetSelectedStage(stageNum);
            });
        }
    }

}
