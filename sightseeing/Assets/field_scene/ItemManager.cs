using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  treeManagerに相当する
//  treeシーンで使用される想定
//  ネーミングはちょっと間違えた気がする
//
public class ItemManager : MonoBehaviour
{

    //
    private void Awake()
    {

        //scene start時に一度だけよんでおく
        update_tree();

    }
    //なんかうまくいかない6/30
    //private void Awake()
    //{
    //    update_tree();

    //}

    //アイテムツリーの更新
    public void update_tree()
    {
        //Debug.Log("update tree");
        foreach (string cc in _item_beh.gobname_list)
        {
            //名前で取得する
            var tmp_gob = GameObject.Find(cc);
            //  Findだとactiveじゃないものを拾えないので注意 -> 解決 meshrendererのみのON/OFFとした
            //  Debug.Log(cc + " :-> " + tmp_gob );

            //獲得していれば表示する
            if (tmp_gob is not null)
            {
                
                //1 マーカーを表示してもらう
                tmp_gob.SendMessage("Activate_Marker");

                //2 アイテムの本体を表示してもらう
                tmp_gob.SendMessage("Active_ItemRenderer");


            }
        }


        //デバッグ用機能テスト  賞品システムの実装:アイテムを１〜３まであつめたら傘を手にいれる
        prize_for_all_item();


    }

    //デバッグ用
    //  機能テスト：一定の条件を満たした場合に獲得されるアイテムを実装する
    private void prize_for_all_item()
    {

        var tmp_gob2 = GameObject.Find("umbrella1");

        if (_item_beh.gobname_list.Contains("apple1") && _item_beh.gobname_list.Contains("book1") && _item_beh.gobname_list.Contains("cat1"))
        {

            //1
            tmp_gob2.SendMessage("Activate_Marker");

            //2
            //ここはほんらいの形をアタッチしてつかう
            tmp_gob2.SendMessage("Active_ItemRenderer");
        }
        else
        {

            //1
            tmp_gob2.SendMessage("InActivate_Marker");

            //2
            //ここはほんらいのアイテムの形をアタッチしてつかう
            tmp_gob2.SendMessage("InActive_ItemRenderer");

        }
    }





}
