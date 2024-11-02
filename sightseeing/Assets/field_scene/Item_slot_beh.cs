using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//また名前がちがう感じになってしまった
// item_bagが正
//　TAGまでITEM_BAGにしてある
public class Item_slot_beh : MonoBehaviour
{


    private int selected_item_no;

    private TMP_Dropdown Drop_down;

    //ドロップダウンのgobを保持して、initializeをかけるだけ
    //fieldシーンに映るたびに呼び出される
    //
    private void Awake()
    {

        Drop_down = GetComponentInChildren<TMP_Dropdown>();
        //Debug.Log("check drop_down:"+Drop_down);
        load_itemlist_in_bag();

    }

    //持ち物が更新されたら　ここを呼び出すようにする
    //ここcallbackで実装してもいいかも
    public void load_itemlist_in_bag()
    {
        //クリアして
        Drop_down.ClearOptions();
        //item_in_bagの内容を適用する
        Drop_down.AddOptions(_item_beh.item_in_bag);
        

    }



    //選択しているアイテムをUSEする
    public void use_selected_item()
    {

        //選択しているアイテム番号を更新
        //選択しているアイテムNoを取得して保持する
        selected_item_no = Drop_down.value;
        //Debug.Log("selected is :"+commondata.item_in_bag[selected_item_no]);



        //
        var tmp_gob = GameObject.FindGameObjectWithTag("Player");
        _item_beh.set_user_gob(tmp_gob);


        //itemを適用する
        _item_beh.apply_item_for_gob(_item_beh.item_in_bag[selected_item_no]);



        //
        //アイテムをリストから消去する
        _item_beh.item_delete(selected_item_no);



        //syokika ここから
        load_itemlist_in_bag();


    }





}




//---------------------------------------------------------いろいろ検討した

////buttonにアタッチして使用する
////functionから下の関数を走らせるイメージで組んでいく
////機能ごとにメソッドを分けるか、同じメソッドに変数を導入して
////使い回すか　悩みどころ
////アイテム名を保持したり破棄したり更新したりしないといけないので
////item_slotパネルにアタッチしてしようすることにした6/24
////
////ボタンの機能やテキスト表示はスクリプト側から制御したいので
////
////game managerで持ち物リストが更新されたタイミングで表示の更新を行う
////もちものリストは表示するだけなので、考えるようなメソッドはここには設定しない
////たとえばもう持てないこと判定したり、
////アイテムの消費を書いたりするようなことはここでは行わないようにしたい



////外部からアクセスする時にアイテム名が保持されていた方が使いやすい
////　かなと思ったけど、ここでは表示だけを行うようにしたいのでもうReadOnly的にコーディングしていく
////public string Hold_Item_name;

//[SerializeField]
//private GameObject text_item_name;
//[SerializeField]
//private GameObject button_text_1;
//[SerializeField]
//private GameObject button_text_2;
//[SerializeField]
//private GameObject button_text_3;


//private void Start()
//{

//    set_button1_text("USE");
//    set_item_name("test");

//}

//// gameManagerで持ち物りすとが更新されたら
////ここでアイテムリストを取得する
//public void get_item_name()
//{
//    var Hold_Item_name = "";
//    //とりあえずこんな感じ
//    foreach (string cc in commondata.item_in_bag)
//    {
//        Hold_Item_name = cc;
//    }
//}


////アイテムの名前を受け取って、スロットの表示に反映する
//public void set_item_name(string Hold_Item_name)
//{
//    Debug.Log("set item name:" + Hold_Item_name);
//    text_item_name.GetComponent<TextMeshProUGUI>().text = Hold_Item_name;
//}


////ボタンのメソッドに名前をセットする
////このときメソッドを仕込むみたいなことができれば尚いいが
//private void set_button1_text(string method_name) { button_text_1.GetComponent<TextMeshProUGUI>().text = method_name; }
//private void set_button2_text(string method_name) { button_text_2.GetComponent<TextMeshProUGUI>().text = method_name; }
//private void set_button3_text(string method_name) { button_text_3.GetComponent<TextMeshProUGUI>().text = method_name; }



