using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropItemAnchorBeh : MonoBehaviour
{
    [SerializeField]
    private GameObject marker_gob;
    [SerializeField]
    private MeshRenderer ItemRenderer_mr;

    private string my_path;
    //objの初期化 シーンを立ち上げた時に毎回呼び出されるので注意
    void Start()
    {

        


        //UIを非表示にして
        InActive_Display();

        //マーカーに呼び出し先となる親objに自分自身を代入して登録しておく
        marker_gob.GetComponent<ItemMarkerBeh>().parent_gob = this.gameObject;

    }


    private void Awake()
    {
        Debug.Log("awake on :get full path()="+gameObject.GetFullPath());
        //フルパス
        my_path = gameObject.GetFullPath();

        //すでに取得済みリストに含まれていたらdestroyする
        if (_item_beh.already_get_gob.Contains(my_path))
        {
            Debug.Log("already get.so delete:" + gameObject.name);
            Destroy(this.gameObject);

        }
    }



    //UIでの 終了&ゲット ボタン
    public void push_get_button_ui()
    {
        //UIを非表示にして
        InActive_Display();

        //gameobjの名前そのものを取得
        var item_name = gameObject.name;

        //データベースの所持アイテムリストにitem名で登録する
        _item_beh.add_item_for_bag(item_name);

        //なんかリストにうまくフルパスでとうろくされていないここから9/5
        //すでに取得済みリストに追加
        _item_beh.already_get_gob.Add(gameObject.GetFullPath());
        //Debug.Log("Add list ::"+gameObject.GetFullPath());
        //いったんここで消す
        Destroy(this.gameObject);

    }


    //UIでの終了キャンセルボタン
    public void push_cancel_button_ui()
    {

        //UIを非表示にして
        InActive_Display();

        //gameobjの名前そのものを取得
        var item_name = gameObject.name;

        //データベースの確認アイテムリストにitem名で登録する
        _item_beh.add_item_for_list(item_name);

    }

    //UIを表示する
    private void Active_Display(){
        gameObject.GetComponentInChildren<Canvas>().enabled = true;
        Camera.main.TryGetComponent<Camera_Beh>(out var eee);
        if (eee is not null) { eee.switch_view2(); }


    }
    //UIを非表示にする
    private void InActive_Display(){
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        Camera.main.TryGetComponent<Camera_Beh>(out var eee);
        if (eee is not null) { eee.switch_view1(); }

    }


    //
    //  よく使う関数群まとめ
    //
    //マーカーを有効化する
    private void Activate_Marker() { marker_gob.SetActive(true); }
    //マーカーを無効化する
    private void InActivate_Marker() { marker_gob.SetActive(false); }
    //アイテム全体を表示する
    private void Active_ItemRenderer() { ItemRenderer_mr.enabled = true; }
    //アイテム全体を非表示にする
    private void InActive_ItemRenderer() { ItemRenderer_mr.enabled = false; }






}
