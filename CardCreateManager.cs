using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardCreateManager : MonoBehaviour
{
    //生成するCardオブジェクト
    public Card CardPrefab;

    //「カード」を生成する親オブジェクト
    public RectTransform CardCreateParent;

    //生成したカードオブジェクトを保存する
    public List<Card> CardList = new List<Card> ();

    // カードを生成し終わったとき
    public Action OnCardCreateComp;

    void Start () {

    }

    public void CreateCard () {

        foreach (Transform child in this.CardCreateParent) {
            Destroy (child.gameObject);
        }
        //Debug.Log ("Destroy!");

        //生成したカードリストをクリアする
        this.CardList.Clear();
        //Debug.Log("Clear!");

        // カード情報リスト
        // スペード
        List<CardData> spadeCardDataList = new List<CardData>();
        List<CardData> clovaCardDataList = new List<CardData>();
        List<CardData> diaCardDataList = new List<CardData>();
        List<CardData> heartCardDataList = new List<CardData>();

        // 表示するカード画像情報のリスト
        // スペード
        List<Sprite> spadeImgList = new List<Sprite>();
        // クローバー
        List<Sprite> clovaImgList = new List<Sprite>();
        // ダイヤ
        List<Sprite> diaImgList = new List<Sprite>();
        // ハート
        List<Sprite> heartImgList = new List<Sprite>();

        //Resource/Imageフォルダ内にある画像を取得する
        // スペード
        spadeImgList.Add(Resources.Load<Sprite>("Image/s01"));//0
        spadeImgList.Add(Resources.Load<Sprite>("Image/s02"));//1
        spadeImgList.Add(Resources.Load<Sprite>("Image/s03"));//2
        spadeImgList.Add(Resources.Load<Sprite>("Image/s04"));//3
        spadeImgList.Add(Resources.Load<Sprite>("Image/s05"));//4
        spadeImgList.Add(Resources.Load<Sprite>("Image/s06"));//5
        spadeImgList.Add(Resources.Load<Sprite>("Image/s07"));//6
        spadeImgList.Add(Resources.Load<Sprite>("Image/s08"));//7
        spadeImgList.Add(Resources.Load<Sprite>("Image/s09"));//8
        spadeImgList.Add(Resources.Load<Sprite>("Image/s10"));//9
        spadeImgList.Add(Resources.Load<Sprite>("Image/s11"));//10
        spadeImgList.Add(Resources.Load<Sprite>("Image/s12"));//11
        spadeImgList.Add(Resources.Load<Sprite>("Image/s13"));//12
        spadeImgList.Add(Resources.Load<Sprite>("Image/joker"));//13
        // クローバ
        clovaImgList.Add(Resources.Load<Sprite>("Image/c01"));//0
        clovaImgList.Add(Resources.Load<Sprite>("Image/c02"));//1
        clovaImgList.Add(Resources.Load<Sprite>("Image/c03"));//2
        clovaImgList.Add(Resources.Load<Sprite>("Image/c04"));//3
        clovaImgList.Add(Resources.Load<Sprite>("Image/c05"));//4
        clovaImgList.Add(Resources.Load<Sprite>("Image/c06"));//5
        clovaImgList.Add(Resources.Load<Sprite>("Image/c07"));//6
        clovaImgList.Add(Resources.Load<Sprite>("Image/c08"));//7
        clovaImgList.Add(Resources.Load<Sprite>("Image/c09"));//8
        clovaImgList.Add(Resources.Load<Sprite>("Image/c10"));//9
        clovaImgList.Add(Resources.Load<Sprite>("Image/c11"));//10
        clovaImgList.Add(Resources.Load<Sprite>("Image/c12"));//11
        clovaImgList.Add(Resources.Load<Sprite>("Image/c13"));//12
        clovaImgList.Add(Resources.Load<Sprite>("Image/joker"));//13
        // ダイア
        diaImgList.Add(Resources.Load<Sprite>("Image/d01"));//0
        diaImgList.Add(Resources.Load<Sprite>("Image/d02"));//1
        diaImgList.Add(Resources.Load<Sprite>("Image/d03"));//2
        diaImgList.Add(Resources.Load<Sprite>("Image/d04"));//3
        diaImgList.Add(Resources.Load<Sprite>("Image/d05"));//4
        diaImgList.Add(Resources.Load<Sprite>("Image/d06"));//5
        diaImgList.Add(Resources.Load<Sprite>("Image/d07"));//6
        diaImgList.Add(Resources.Load<Sprite>("Image/d08"));//7
        diaImgList.Add(Resources.Load<Sprite>("Image/d09"));//8
        diaImgList.Add(Resources.Load<Sprite>("Image/d10"));//9
        diaImgList.Add(Resources.Load<Sprite>("Image/d11"));//10
        diaImgList.Add(Resources.Load<Sprite>("Image/d12"));//11
        diaImgList.Add(Resources.Load<Sprite>("Image/d13"));//12
        // ハート
        heartImgList.Add(Resources.Load<Sprite>("Image/h01"));//0
        heartImgList.Add(Resources.Load<Sprite>("Image/h02"));//1
        heartImgList.Add(Resources.Load<Sprite>("Image/h03"));//2
        heartImgList.Add(Resources.Load<Sprite>("Image/h04"));//3
        heartImgList.Add(Resources.Load<Sprite>("Image/h05"));//4
        heartImgList.Add(Resources.Load<Sprite>("Image/h06"));//5
        heartImgList.Add(Resources.Load<Sprite>("Image/h07"));//6
        heartImgList.Add(Resources.Load<Sprite>("Image/h08"));//7
        heartImgList.Add(Resources.Load<Sprite>("Image/h09"));//8
        heartImgList.Add(Resources.Load<Sprite>("Image/h10"));//9
        heartImgList.Add(Resources.Load<Sprite>("Image/h11"));//10
        heartImgList.Add(Resources.Load<Sprite>("Image/h12"));//11
        heartImgList.Add(Resources.Load<Sprite>("Image/h13"));//12

        // forを回す回数を取得する
        // スペード
        int spadeLoopCnt = spadeImgList.Count;
        // クローバ
        int clovaLoopCnt = clovaImgList.Count;
        // ダイア
        int diaLoopCnt = diaImgList.Count;
        // ハート
        int heartLoopCnt = heartImgList.Count;

        // スペード
        for(int i = 0; i < spadeLoopCnt; i++) {
             
            //カード情報を生成する
            CardData spadeCarddata = new CardData (i, ESuit.SPADE, spadeImgList[i]);
            spadeCardDataList.Add(spadeCarddata);
        }
        // クローバ
        for(int i = 0; i < clovaLoopCnt; i++) {
             
            //カード情報を生成する
            CardData clovaCarddata = new CardData (i, ESuit.CLOVA, clovaImgList[i]);
            clovaCardDataList.Add(clovaCarddata);
        }
        // ダイア
        for(int i = 0; i < diaLoopCnt; i++) {
             
            //カード情報を生成する
            CardData diaCarddata = new CardData (i, ESuit.DIA, diaImgList[i]);
            diaCardDataList.Add(diaCarddata);
        }
        // ハート
        for(int i = 0; i < heartLoopCnt; i++) {
             
            //カード情報を生成する
            CardData heartCarddata = new CardData (i, ESuit.HEART, heartImgList[i]);
            heartCardDataList.Add(heartCarddata);
        }

        //生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(spadeCardDataList);
        SumCardDataList.AddRange(clovaCardDataList);
        SumCardDataList.AddRange(diaCardDataList);
        SumCardDataList.AddRange(heartCardDataList);

        //リストの中身をランダムに再配置する
        List<CardData> randomCardDataList = SumCardDataList.OrderBy(a => System.Guid.NewGuid()).ToList();

        // カードオブジェクトを生成する
        foreach (var _cardData in randomCardDataList) {

            var num = UnityEngine.Random.Range(-180f, 180f);
            var randomQ = Quaternion.Euler(0, 0, num);

            // Instantiate で Cardオブジェクトを生成
            Card card = Instantiate<Card> (this.CardPrefab, this.CardCreateParent);
            //Debug.Log ("Instantiate!");

            //データを設定する
            card.Set(_cardData);
            
            //生成したカードオブジェクトを保存する
            this.CardList.Add(card);
        }

        if (this.OnCardCreateComp != null) {
            this.OnCardCreateComp();
        }
    }

    public void HideCardList (List<CardInfo> containCardInfoList) {

        foreach (var _card in this.CardList) {

            var _idSuit = new CardInfo (_card.Id, _card.Suit);
            //Debug.Log("foreach"+ _card.Id + _card.Suit);

            // 既に獲得したカードの場合、非表示にする
            if (containCardInfoList.Contains(_idSuit)) {

                //カードを非表示にする
                _card.SetInvisible ();
            }

            else if (_card.IsSelected) {
                //カードを裏面表示にする
                _card.SetHide ();
            }
        }
    }
}
