using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{   
    /// <summary>
    /// Readyステートに遷移する
    /// </summary>
    public void OnGameStart() {
        // ゲームシーンに遷移
        SceneManager.LoadScene("Game");
        //Debug.Log("Click!");
    }

    /// <summary>
    /// Explainステートに遷移する
    /// </summary>
    public void OnExplain() {
        // 説明シーンに遷移
        SceneManager.LoadScene("Explain");
    }

    /// <summary>
    /// ゲーム終了ボタン
    /// </summary>
    public void OnExit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
