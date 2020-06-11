using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour {
    public Text titleText;
    public GameObject container;
    public Button pauseButton;
    public Button menuButton;
    public Button retryButton;
    public Image backgroundImage;

    // Start is called before the first frame update
    void Start() {
        pauseButton.onClick.AsObservable()
            .Do(_ => {
                var activeSelf = container.activeSelf;
                Time.timeScale = activeSelf ? 1 : 0;
                backgroundImage.enabled = !activeSelf;
                container.SetActive(!activeSelf);
            })
            .Subscribe();

        menuButton.onClick.AsObservable()
            .Do(_ => {
                // 現在のScene名を取得する
                Scene loadScene = SceneManager.GetActiveScene();
                // Sceneの読み直し
                SceneManager.LoadScene(loadScene.name);
            })
            .Subscribe();

        retryButton.onClick.AsObservable()
            .Do(_ => { })
            .Subscribe();
    }

    // Update is called once per frame
    void Update() {

    }
}
