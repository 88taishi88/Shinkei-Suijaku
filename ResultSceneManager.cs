using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{

    // リザルトステートクラス
    public ResultStateManager resultStateManager;

    void Start()
    {
        this.mSetResultState();
    }

    /// <summary>
    /// リザルトステートの設定画面
    /// </summary>
    private void mSetResultState() {

        this.resultStateManager.SetFirstResultText((int)GameSceneManager.firstTurnPoint);
        this.resultStateManager.SetSecondResultText((int)GameSceneManager.secondTurnPoint);
        this.resultStateManager.SetGameResultText((int)GameSceneManager.firstTurnPoint, (int)GameSceneManager.secondTurnPoint);
    }

    /// <summary>
    /// スタート画面に遷移する
    /// </summary>
    public void OnBackStartState() {
        // スタートシーンに遷移
        SceneManager.LoadScene("Start");
        //Debug.Log("Click!");
    }
}
