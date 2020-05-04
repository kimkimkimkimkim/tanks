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

    private float transitionTime = 0.3f;

    void Start() {
        goToStageSelectButton.onClick.AsObservable()
            .Do(_ => {
                var list = new List<Transform>();
                list.Add(goToStageSelectButton.transform);
                list.Add(optionButton.transform);
                list.Add(statsButton.transform);
                list.Add(infoButton.transform);
                HideLeft(list);

                var showList = new List<Transform>();
                SetStageSelectScroll();
                showList.Add(stageSelectScrollView.transform);
                ShowLeft(showList);

                var fadeInList = new List<Transform>();
                fadeInList.Add(playButton.transform);
                fadeInList.Add(backButtonInStageSelect.transform);
                FadeIn(fadeInList);
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
                ShowLeft(showList);

                var hideList = new List<Transform>();
                hideList.Add(stageSelectScrollView.transform);
                HideLeft(hideList);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(playButton.transform);
                fadeOutList.Add(backButtonInStageSelect.transform);
                FadeOut(fadeOutList);
            })
            .Subscribe();

        playButton.onClick.AsObservable()
            .Do(_ => {
                gameManager.GameStart();

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(titleCanvas.transform);
                FadeOut(fadeOutList);
            })
            .Subscribe();
    }

    private void HideLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            float distance = 1218f;
            transform.DOLocalMoveX(x - distance, transitionTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => {
                    transform.gameObject.SetActive(false);
                    transform.localPosition = new Vector3(x,y,z);
                });
        }
    }

    private void ShowLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            float distance = 1218f;
            transform.localPosition = new Vector3(x - distance,y,z);
            transform.gameObject.SetActive(true);
            transform.DOLocalMoveX(x,transitionTime)
                .SetEase(Ease.InCubic)
                .OnComplete(() => {

                });
        }
    }

    private void FadeIn(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            if(!transform.gameObject.GetComponent<CanvasGroup>())transform.gameObject.AddComponent<CanvasGroup>();
            transform.gameObject.SetActive(true);
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            transform.gameObject.GetComponent<CanvasGroup>().DOFade(1,transitionTime).SetEase(Ease.InCubic);
        }
    }

    private void FadeOut(List<Transform> transformList) {
      foreach (Transform transform in transformList) {
          if(!transform.gameObject.GetComponent<CanvasGroup>())transform.gameObject.AddComponent<CanvasGroup>();
          transform.gameObject.SetActive(true);
          transform.gameObject.GetComponent<CanvasGroup>().alpha = 1;
          transform.gameObject.GetComponent<CanvasGroup>()
              .DOFade(0,transitionTime)
              .SetEase(Ease.InCubic)
              .OnComplete(() => transform.gameObject.SetActive(false));
      }
    }

    private void SetStageSelectScroll(){
        int allStageNum = 33;
        for(int i=0;i<allStageNum;i++){
            GameObject stageSelectScrollItem = (GameObject)Instantiate(stageSelectScrollItemPrefab);
            stageSelectScrollItem.transform.SetParent(stageSelectScrollContent.transform);
            stageSelectScrollItem.transform.localScale = new Vector3(1,1,1);

            Button button = stageSelectScrollItem.GetComponent<Button>();
            Text text = stageSelectScrollItem.transform.GetChild(0).GetComponent<Text>();
            Image lockIcon = stageSelectScrollItem.transform.GetChild(1).GetComponent<Image>();

            button.interactable = (i == 0);
            text.text = "LEVEL " + (i+1).ToString();
            lockIcon.gameObject.SetActive(i!=0);
        }
    }

}
