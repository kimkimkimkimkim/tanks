using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimationManager : MonoBehaviour {
    public static float transitionTime = 0.3f;

    public static void HideLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            float distance = 1218f;
            transform.DOLocalMoveX(x - distance, transitionTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => {
                    transform.gameObject.SetActive(false);
                    transform.localPosition = new Vector3(x, y, z);
                });
        }
    }

    public static void ShowLeft(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            float distance = 1218f;
            transform.localPosition = new Vector3(x - distance, y, z);
            transform.gameObject.SetActive(true);
            transform.DOLocalMoveX(x, transitionTime)
                .SetEase(Ease.InCubic)
                .OnComplete(() => {

                });
        }
    }

    public static void FadeIn(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            if (!transform.gameObject.GetComponent<CanvasGroup>()) transform.gameObject.AddComponent<CanvasGroup>();
            transform.gameObject.SetActive(true);
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            transform.gameObject.GetComponent<CanvasGroup>().DOFade(1, transitionTime).SetEase(Ease.InCubic);
        }
    }

    public static void FadeOut(List<Transform> transformList) {
        foreach (Transform transform in transformList) {
            if (!transform.gameObject.GetComponent<CanvasGroup>()) transform.gameObject.AddComponent<CanvasGroup>();
            transform.gameObject.SetActive(true);
            transform.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            transform.gameObject.GetComponent<CanvasGroup>()
                .DOFade(0, transitionTime)
                .SetEase(Ease.InCubic)
                .OnComplete(() => transform.gameObject.SetActive(false));
        }
    }
}
