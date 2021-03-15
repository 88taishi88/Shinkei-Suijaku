using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    //一致したカードリストID
    private List<CardInfo> mContainCardInfoList = new List<CardInfo>();

    //カード生成マネージャクラス
    public CardCreateManager CardCreate;

    // ゲームステートクラス
    public GameStateManager gameStateManager;

    // ゲームステート管理
    private EGameState mEGameState;

    // ターンの定義
    private const int FIRST = 1;
    private const int SECOND = 2;

    // 現在のターン
    private int currentTurn;

    // ターンポイント設定
    // 先攻
    public static int firstTurnPoint = 0;
    // 後攻
    public static int secondTurnPoint = 0;

    void Start() {

        // ゲームステートを初期化
        this.mEGameState = EGameState.READY;

        // ゲームステートを非表示
        this.gameStateManager.gameObject.SetActive(false);

        // ゲームのステート管理
        this.mSetGameState();

        // 現在のターンを定義
        currentTurn = FIRST;
    }

    void Update() {

        // GameState が GAME状態なら
        if (this.mEGameState == EGameState.GAME) {

            this.mSetGameText();

            //選択したカードが二枚以上になったら
            if (GameStateController.Instance.SelectedCardInfoList.Count >= 2) {

                // 最初に選択したCardIDとスートを取得する
                CardInfo firstSelectedCardInfo = GameStateController.Instance.SelectedCardInfoList[0];
                // 二番目に選択したCardIDとスートを取得する
                CardInfo secondSelectedCardInfo = GameStateController.Instance.SelectedCardInfoList[1];

                //二枚目にあったカードと一緒だったら
                if (firstSelectedCardInfo.Id == secondSelectedCardInfo.Id) {

                    //Debug.Log($"Contains! {firstSelectedIdSuit.Id}");
                    //一致したカードIDスートを保存する
                    this.mContainCardInfoList.Add(firstSelectedCardInfo);
                    this.mContainCardInfoList.Add(secondSelectedCardInfo);

                    PointAdd(firstSelectedCardInfo.Id);

                } else {
                    // ターン切り替えを行う
                    TurnSwitch();
                }

                // カードの表示切替を行う
                this.CardCreate.HideCardList(this.mContainCardInfoList);

                //選択したカードリストを初期化する
                GameStateController.Instance.SelectedCardInfoList.Clear();
            }

            // 配置した全種類のカードを獲得したら
            if (this.mContainCardInfoList.Count >= 54) {

                // ゲームをリザルトステートに遷移する
                SceneManager.LoadScene("Result");
            }
        }
    }

    /// <summary>
    /// ゲーム画面のテキスト設定
    /// </summary>
    private void mSetGameText() {

        // 先攻点数をテキストに反映
        this.gameStateManager.SetFirstPointText((int)firstTurnPoint);
        // 後攻点数をテキストに反映
        this.gameStateManager.SetSecondPointText((int)secondTurnPoint);
        // 現在ターンをテキストに反映
        if (currentTurn == FIRST) {
                this.gameStateManager.SetCurrentText("先攻");
            } else if (currentTurn == SECOND) {
                this.gameStateManager.SetCurrentText("後攻");
            }
    }

    /// <summary>
    /// ゲームステートで処理を変更する
    /// </summary>
    private void mSetGameState() {

        switch (this.mEGameState) {
            //ゲーム準備期間
            case EGameState.READY:
                // ゲームの準備ステートを開始する
                this.mSetGameReady();
                break;
            //ゲーム中
            case EGameState.GAME:
                this.gameStateManager.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// ゲームの準備ステートを開始する
    /// </summery>
    private void mSetGameReady() {

        // カード配布アニメーションが終了した後のコールバック処理を実装する
        this.CardCreate.OnCardCreateComp = null;
        this.CardCreate.OnCardCreateComp = () => {

            // ゲームステートをGAME状態にする
            this.mEGameState = EGameState.GAME;
            this.mSetGameState();
        };

        // 一致したカードIDリストを初期化
        this.mContainCardInfoList.Clear();

        // カードリストを生成する
        this.CardCreate.CreateCard();

        // 現在のターンを初期化
        currentTurn = FIRST;

        // ポイント初期化
        // 先攻
        firstTurnPoint = 0;
        // 後攻
        secondTurnPoint = 0;
    }

    /// <summary>
    /// ターン切り替え
    /// </summary>
    private void TurnSwitch() {
        if (currentTurn == FIRST) {
            currentTurn = SECOND;
        } else if (currentTurn == SECOND) {
            currentTurn = FIRST;
        }
    }

    /// <summary>
    /// ポイント加算処理
    /// </summary>
    private void DefaultPointAdd() {
        if (currentTurn == FIRST) {
            firstTurnPoint += 1;
        } else if (currentTurn == SECOND) {
            secondTurnPoint += 1;
        }
    }

    /// <summary>
    /// 特殊効果処理
    /// </summary>
    private void PointAdd(int id) {

        switch(id) {
            case 1:
                if (currentTurn == FIRST) {
                    firstTurnPoint = firstTurnPoint * 2;
                } else if (currentTurn == SECOND) {
                    secondTurnPoint = secondTurnPoint * 2;
                }
                break;
            case 2:
                if (currentTurn == FIRST) {
                    firstTurnPoint = (int)Math.Pow((double)firstTurnPoint, 2);
                } else if (currentTurn == SECOND) {
                    secondTurnPoint = (int)Math.Pow((double)secondTurnPoint, 2);
                }
                break;
            case 12:
                if (currentTurn == FIRST) {
                    int fKingPoint = (int)Math.Floor((double)(secondTurnPoint / 2));
                    firstTurnPoint = firstTurnPoint + fKingPoint;
                    secondTurnPoint = secondTurnPoint - fKingPoint;
                } else if (currentTurn == SECOND) {
                    int sKingPoint = (int)Math.Floor((double)(firstTurnPoint / 2));
                    secondTurnPoint = secondTurnPoint + sKingPoint;
                    firstTurnPoint = firstTurnPoint - sKingPoint;
                }
                break;
            case 13:
                int mFirstTurnPoint = firstTurnPoint;
                int mSecondTurnPoint = secondTurnPoint;

                firstTurnPoint = mSecondTurnPoint;
                secondTurnPoint = mFirstTurnPoint;
                break;
            default:
                DefaultPointAdd();
                break;
        }
    }
}