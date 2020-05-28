using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ResultScreenManager : MonoBehaviour {

    public GameManager gameManager;
    public Button menuButton;
    public Button retryButton;
    public Button nextButton;
    public GameObject starIconContainer;
    public Color onStarColor;
    public Color offStarColor;

    public void SetScreen(int star, int stageNumber) {
        if (!transform.gameObject.GetComponent<CanvasGroup>()) transform.gameObject.AddComponent<CanvasGroup>();
        transform.gameObject.SetActive(true);
        transform.gameObject.GetComponent<CanvasGroup>().alpha = 1;

        SetSaveData(star, stageNumber);
        SetScore(star);
        ShowNextButton(stageNumber);

        menuButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();

        retryButton.onClick.AsObservable()
            .Do(_ => {
                gameManager.GameStart(stageNumber);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(gameObject.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();

        nextButton.onClick.AsObservable()
            .Do(_ => {
                gameManager.GameStart(stageNumber + 1);

                var fadeOutList = new List<Transform>();
                fadeOutList.Add(gameObject.transform);
                UIAnimationManager.FadeOut(fadeOutList);
            })
            .Subscribe();
    }

    private void SetSaveData(int star, int stageNumber) {
        if (star == 0) return;

        List<StageData> clearStageDataList = SaveData.GetClassList(SaveDataKey.clearStageDataList, SaveDataDefaultValue.clearStageDataList);
        if (stageNumber > clearStageDataList.Count) {
            //初めてクリア
            clearStageDataList.Add(new StageData(stageNumber, star));
        } else {
            //以前にクリアしたことあるのでスコアが高ければ保存
            StageData stageData = clearStageDataList[stageNumber - 1];
            if (star > stageData.evaluation) {
                clearStageDataList[stageNumber - 1].evaluation = star;
            }
        }
        SaveData.SetClassList(SaveDataKey.clearStageDataList, clearStageDataList);
    }

    private void SetScore(int star) {
        for (int i = 0; i < starIconContainer.transform.childCount; i++) {
            Color color = (i < star) ? onStarColor : offStarColor;
            starIconContainer.transform.GetChild(i).GetComponent<Image>().color = color;
        }
    }

    private void ShowNextButton(int stageNumber) {
        bool isShow = stageNumber != gameManager.stagePrefabList.Count;
        nextButton.gameObject.SetActive(isShow);
    }
}
