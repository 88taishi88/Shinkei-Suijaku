using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    // 先攻ポイント
    public Text FirstPointText;

    // 後攻ポイント
    public Text SecondPointText;

    // 現在ターン
    public Text CurrentTurnText;

    /// <summary>
    /// 先攻ポイントを表示
    /// </summary>
    public void SetFirstPointText (int point) {
        this.FirstPointText.text = "先攻 : " + point + "点";
    }

    /// <summary>
    /// 後攻ポイントを表示
    /// </summary>
    public void SetSecondPointText (int point) {
        this.SecondPointText.text = "後攻 : " + point + "点";
    }

    /// <summary>
    /// 現在ターン表示
    /// </summary>
    public void SetCurrentText (string currentTurn) {
        this.CurrentTurnText.text = currentTurn;
    }
}
