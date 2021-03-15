using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStateManager : MonoBehaviour
{
    // 時間を表示するテキスト
    public Text TimerText;

    //先攻結果
    public Text FirstResult;

    //後攻結果
    public Text SecondResult;

    //試合結果
    public Text GameResult;

    /// <summary>
    /// 先攻の結果を表示する
    /// </summary>
    public void SetFirstResultText (int point) {
        this.FirstResult.text = "先攻：" + point;
    }

    /// <summary>
    /// 後攻の結果を表示する
    /// </summary>
    public void SetSecondResultText (int point) {
        this.SecondResult.text = "後攻：" + point;
    }

    /// <summary>
    /// ゲームの結果を表示する
    /// </summary>
    public void SetGameResultText (int firstPoint, int secondPoint) {
        if (firstPoint > secondPoint) {
            this.GameResult.text = "先攻の勝利！";
        } else if (firstPoint < secondPoint) {
            this.GameResult.text = "後攻の勝利！";
        } else if (firstPoint == secondPoint) {
            this.GameResult.text = "引き分け！";
        }
    }
}
