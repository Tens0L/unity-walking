using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// item本体側
//
public class ItemAnchorBeh : MonoBehaviour
{
    [SerializeField]
    private GameObject marker_gob;
    [SerializeField]
    private MeshRenderer ItemRenderer_mr;

    //objの初期化 シーンを立ち上げた時に毎回呼び出されるので注意
    void Start()
    {

        


        //UIを非表示にして
        InActive_Display();

        //マーカーに呼び出し先となる親objに自分自身を代入して登録しておく
        marker_gob.GetComponent<ItemMarkerBeh>().parent_gob = this.gameObject;
    }

    //UIでの終了キャンセルボタン
    public void push_cancel_button_ui()
    {


        //UIを非表示にして
        InActive_Display();



        //Debug.Log(gameObject.name);
        //gameobjの名前そのものを取得
        var item_name = gameObject.name;
        //Debug.Log("name is :"+item_name);

        //データベースの所持アイテムリストにitem名で登録する
        _item_beh.add_item_for_list(item_name);

    }

    //UIを表示する
    private void Active_Display()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = true;
        Camera.main.TryGetComponent<Camera_Beh>(out var eee);
        if (eee is not null) { eee.switch_view2(); }

    }
    //UIを非表示にする
    private void InActive_Display()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;

        //field sceneのときだけカメラスイッチ入れたいので
        //いまはこうしている。のちのちシーンによって
        //振る舞いをカメラ側で書き換える必要あるかも
        Camera.main.TryGetComponent<Camera_Beh>(out var eee);
        if (eee is not null) { eee.switch_view1(); }

    }


    //
    //  よく使う関数群まとめ
    //
    //マーカーを有効化する
    private void Activate_Marker() { marker_gob.SetActive(true);}
    //マーカーを無効化する
    private void InActivate_Marker() { marker_gob.SetActive(false);}

    //アイテム全体を表示する
    private void Active_ItemRenderer(){ ItemRenderer_mr.enabled = true; }
    //アイテム全体を非表示にする
    private void InActive_ItemRenderer(){ ItemRenderer_mr.enabled = false; }

 

}
