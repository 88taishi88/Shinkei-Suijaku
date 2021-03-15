using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplainSceneManager : MonoBehaviour
{
    /// <summary>
    /// Startステートに遷移する
    /// </summary>
    public void OnBack() {
        // スタートシーンに戻る
        SceneManager.LoadScene("Start");
    }
}
