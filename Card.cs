using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    //カードのID
    public int Id;

    // カードのスート
    public ESuit Suit;

    //表示するカードの画像
    public Image CardImage;

    // 透過処理用
    public CanvasGroup CanGroup;

    //選択されているか判定
    private bool mIsSelected = false;

    public bool IsSelected => this.mIsSelected;

    //カード情報
    private CardData mData;

    // 座標情報
    private RectTransform mRt;

    //カードの設定
    public void Set(CardData data) {

        //カード情報を設定
        this.mData = data;

        //IDを設定する
        this.Id = data.Id;

        // スートを設定する
        this.Suit = data.Suit;

        //表示する画像を設定する
        //初回はすべて裏面表示とする
        this.CardImage.sprite = Resources.Load<Sprite>("Image/TrumpBack");

        //選択判定フラグを初期化する
        this.mIsSelected = false;

        // アルファ値を1に設定
        this.CanGroup.alpha = 1;
        if (this.CanGroup.alpha == 1){
            //Debug.Log("alpha=1!");
        }

        // 座標情報を取得しておく
        this.mRt = this.GetComponent<RectTransform> ();
    }

    //選択された時の処理
    public void OnClick () {

        //カードが表面になっていた場合は無効
        if(this.mIsSelected) {
            return;
        }

        //Debug.Log("OnClick");

        //回転処理を行う
        this.onRotate (() => {
            //選択判定フラグを有効にする
            this.mIsSelected = true;

            //カードを表面にする
            this.CardImage.sprite = this.mData.ImgSprite;

            // Y座標を元に戻す
            this.onReturnRotate (() => {
                //選択したCardIdを保存する
                CardInfo selectedCardInfo = new CardInfo (this.mData.Id, this.mData.Suit);
                GameStateController.Instance.SelectedCardInfoList.Add(selectedCardInfo);
            });
        });
    }

    public void SetHide () {

        // 0.5秒待ってから裏返す
        StartCoroutine(WaitHide());

    }

    public void SetInvisible () {
        
        // 選択済み判定にする
        this.mIsSelected = true;

        //アルファ値を0に設定（非表示）
        this.CanGroup.alpha = 0;
    }

    private void onRotate (Action onComp) {

        //90度回転する
        this.mRt.DORotate (new Vector3 (0f, 90f, 0f), 0.2f)
            // 回転が終了したら
            .OnComplete (() => {

                if (onComp != null) {
                    onComp ();
                }
            });
    }

    private void onReturnRotate (Action onComp) {

        this.mRt.DORotate (new Vector3 (0f, 0f, 0f), 0.2f)
            //回転が終わったら
            .OnComplete (() => {

                if (onComp != null) {
                    onComp ();
                }
            });
    }

    /// <summary>
    /// 0.5秒待って裏返す処理
    /// </summary>
    IEnumerator WaitHide()
    {
        // 0.5秒待つ
        yield return new WaitForSeconds(0.3f);

        // 90度回転する
        this.onRotate(() => {
            // 選択判定フラグを初期化する
            this.mIsSelected = false;

            // カードを背面表示にする
            this.CardImage.sprite = Resources.Load<Sprite>("Image/TrumpBack");

            // 角度を元に戻す
            this.onReturnRotate(() => {
                //Debug.Log("onhide");
            });
        });
    }
}

public class CardData {
    //カードID
    public int Id { get; private set; }

    //スート
    public ESuit Suit { get; private set; }

    //画像
    public Sprite ImgSprite { get; private set; }
    
    public CardData (int _id, ESuit _suit, Sprite _sprite) {
        this.Id = _id;
        this.Suit = _suit;
        this.ImgSprite = _sprite;
    }
}

public struct CardInfo {
    // カードID
    public int Id { get; private set; }

    // スート
    public ESuit Suit { get; private set; }

    public CardInfo (int _id, ESuit _suit) {
        this.Id = _id;
        this.Suit = _suit;
    }
}