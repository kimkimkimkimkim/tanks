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
        gameObject.SetActive(true);
        SetScore(star);

        menuButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();

        retryButton.onClick.AsObservable()
            .Do(_ => { })
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

    private void SetScore(int star) {
        for (int i = 0; i < starIconContainer.transform.childCount; i++) {
            Color color = (i < star) ? onStarColor : offStarColor;
            starIconContainer.transform.GetChild(i).GetComponent<Image>().color = color;
        }
    }
}
