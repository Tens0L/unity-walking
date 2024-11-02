using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Events;//コールバックテスト
//大切なデータはここ


//------------------------------------------------
//    アイテム管理クラス
//------------------------------------------------
//item系はこちらに分離することにした　ここから
public static class _item_beh
{
    public static GameObject item_apply_for; //アイテム使用対象gobをここに保持しておく用
    public static List<string> gobname_list;
    public static List<string> item_in_bag;

    //このdbは毎回作ればいい　対応表を参照するだけ
    public static List<string> drop_item_db;
    //ここはstringで保存してみる
    public static List<string> already_get_gob;

    public static void init_for_start()
    {
        //発見したアイテムリスト
        _item_beh.gobname_list = new List<string>();
        //持っているアイテムリスト
        _item_beh.item_in_bag = new List<string>();

        //過去に取得したアイテムリスト
        _item_beh.already_get_gob = new List<string>();

    }

    public static void add_item_for_list(string tmp)
    {
        gobname_list.Add(tmp);
        //addしたらsort
        item_sort();
    }

    public static void add_item_for_bag(string tmp)
    {


        item_in_bag.Add(tmp);
        //addしたらsort
        item_sort();

        var item_bag = GameObject.FindGameObjectWithTag("ITEM_BAG");
        //Debug.Log(item_bag); 
        if (item_bag is not null)
        {
            //itemBagへメッセージする
            item_bag.SendMessage("load_itemlist_in_bag");
        }


        //これはExecuteEventsの書き方
        //ちょっと機能が豊富すぎるのでいまは不採用とした
        //ExecuteEvents.Execute<UpdateBagRecieveMessage>(
        //    target: item_bag,
        //        null,
        //        (recieveTarget, y) => recieveTarget.OnRecieve());

    }



    //アイテム使用対象キャラクターのgobを保持するメソッド
    //アイテムスロットUI側から呼び出して使用する
    //
    public static void set_user_gob(GameObject gob)
    {
        item_apply_for = gob;
    }

    //アイテムの効果を対象に使用する
    //valueはアイテム効果の番号を登録する
    //ユーザーにアイテムを適用するようなかんじ
    //delegate実装はあとまわし
    public static void apply_item_for_gob(string item_name)
    {

        if (item_name == "energy_candy")
        {
            Debug.Log("use energy_candy");
            //ここ細かい効果とかはuser beh側に書いた方がいいかも
            item_apply_for.GetComponent<UserBeh>().energy_point += 1000;
            item_apply_for.GetComponent<UserBeh>().update_player_HUD();
        }

        if (item_name == "jump_candy")
        {
            Debug.Log("use jump_candy");
            item_apply_for.transform.Translate(new Vector3(5f, 0f, 0f));
        }
    }


    //
    //アイテムを使用したことでアイテムが消費・消滅する
    //
    public static void item_delete(int item_no)
    {
        //item_bagの中の該当番号に””をいれてアイテム消す
        item_in_bag[item_no] = "";

        //use&deleteしたらsort
        item_sort();
    }

    public static void item_sort()
    {
        //名前が統一感なくてやりづらい　listのなまえちょっと考える
        gobname_list.Sort();
        var tmp_list=gobname_list.Distinct().ToList();
        gobname_list = tmp_list;
        gobname_list.RemoveAll(value => value == "");

        item_in_bag.Sort();
        item_in_bag.RemoveAll(value => value == "");

    }
}


//------------------------------------------------
//  ゲーム全体パラメータ・データ管理クラス
//------------------------------------------------

public static class commondata
{
    //playerの変化パラメータ
    public static Vector3 player_xyz;
    public static int energy_point;

    //playerの固定パラメータ
    //fieldで歩くスピード　とかここで定義したい


    public static void  init_for_start()
    {
        //item関連の初期化
        _item_beh.init_for_start();

        //newgameしたときの初期位置
        //こういう情報はどこかにまとめた方がいい気がする。あとで。
        //player_xyz = new Vector3(20f, 1.5f, 6f); // 部屋のスロット前
        player_xyz = new Vector3(105f, 112f, 107f); // はじまりの道

        energy_point = 10000;//
    }

    //player state
    public enum PLAYER_STATE
    {
        active,
        stop
    }
}

///
/// ゲームマネージャー ルール管理クラス
/// ゲームの主要な流れをコーディングする
/// ゲームクリア、ゲームオーバー、タイトルメニューからのシーン遷移
/// タイトルメニューからのシーン遷移って分けれるなら分けたほうがよいかも
/// と思っていたが
/// save / loadはゲームレギュレーションにかかわるし
/// いまはここが便利なのでここはこのまま作業進める
/// 
public class GameManager : MonoBehaviour
{

    public float wall_clock_time;
    private int step_count;


    //tick time stepをコールバックで実装する
    public delegate void TickTimeStep();
    public TickTimeStep tickHandler;

    public List<GameObject> tick_reciever;
    //このrick_reciever listが初期化されていないのでstartで初期化走らせる



    //------------------------------------------------
    //  初期化　仕込み　関連
    //------------------------------------------------
    //



    private void Start()
    {
        
        //fieldシーンのときだけプレイヤーオブジェクトが存在するので
        //このときだけゲームオーバーのコールバックをしこみたい
        if (SceneManager.GetActiveScene().name == "field")
        {
            var tmp_gob = GameObject.FindGameObjectWithTag("Player");

            //ゲームオーバーを仕込む
            tmp_gob.GetComponent<UserBeh>().deathHandler = GameOver;

            //Playerの入力にあわせてnext_time_stepがすすむ
            tmp_gob.GetComponent<UserBeh>().nextHandler = Next_time_step;

            //
            wall_clock_time = 12;//90deg=12:00

            //ここでりすと初期化したほうがいいのか検討中9/12ここから
            Debug.Log("---tick_rec="+tick_reciever.ToString());


        }
    }

    //------------------------------------------------
    //  TimeStep 関連
    //------------------------------------------------


    public void Tick_timestep()
    {
        //wall clockは0時から24時まで
        //ここうまく動いてないけど
        // dirction_light sunの角度で１８時調整してるけどそこでうまくいかない。一旦はずしとくか
        //!!!!!一旦保留!!!!!!

        wall_clock_time += 24f/360f;
        if (wall_clock_time >= 24) {
            wall_clock_time = 0;
        }
        //Debug.Log("now::"+wall_clock_time.ToString());



        //Debug.Log("GameManager>Tick_timestep");
        //この関数が走ったことでenemy側で仕込んだ関数が発火する
        tickHandler?.Invoke();



        foreach (var item in tick_reciever)
        {
            //ここ　相手がvisible falseになってしまっているときの対策
            item.TryGetComponent<PointLight_beh>(out var tmp);
            if(tmp is not null)
            {
                tmp.tick_timestep();
            }

        }


    }

    //------------------------------------------------
    //  メニュー 関連
    //------------------------------------------------
    // ゲームクリア時に呼ばれるようにしたい
    public void ClearGame()
    {
        //クリアしたことを表示
        Debug.Log("clear game");
        //Saveして
        save_game_data();
        //titleにもどる
        GetComponentInParent<SceneBeh>().change_scene_End();
    }

    //ゲームオーバー時に呼ばれる
    private void GameOver(string result)
    {
        //ゲームオーバーしたことを表示
        Debug.Log("Game Over :"+result);
        //saveせず
        //titleにもどる
        GetComponentInParent<SceneBeh>().change_scene_Over();
    }
    //time step カウントアップ時に呼ばれる
    private void Next_time_step(int count)
    {
        //
        //Debug.Log("GameManager>Next_time_step():"+count.ToString());
        Tick_timestep();

    }

    //デバッグ用 まだつかってます6/19
    public void DEBUG_add_item_and_save()
    {
        //
        _item_beh.init_for_start();
        _item_beh.add_item_for_list("apple1");
        _item_beh.add_item_for_list("book1");
        _item_beh.add_item_for_list("cat1");
        _item_beh.add_item_for_bag("gomi");
        //
        commondata.player_xyz.x = 20f;
        commondata.player_xyz.y = 1.5f;
        commondata.player_xyz.z = 6f;
        commondata.energy_point = 12345;
        save_game_data();
    }

    //new gameボタンがおされたときの初期化スタート
    //タイトルメニューでnewGameボタンがおされたとき
    public void push_new_game()
    {
        commondata.init_for_start();
    }

    //saveデータの破壊
    //タイトルメニューでbreakボタンがおされたとき
    public void break_save_data()
    {
        PlayerPrefs.DeleteAll();
    }

    //saveデータのロード
    //タイトルメニューでloadボタンが押されたとき
    public void load_game_data()
    {

        //ロードのまえに初期化しておく
        commondata.init_for_start();

        //これはDEBUG用でリストを全て表示するためのもの
        var tmp_text="";

        //全ての発見リストに対してロードでaddをかけていく
        for (int i=0; i<100;i++)
        {
            var tmp = PlayerPrefs.GetString("key" + i.ToString());
            if (tmp == "")
            {
                //保存はkey1,key2,...でそろえてあるので、そこをループする
                //  Debug.Log("key" + i.ToString() + "is null");
            }
            else
            {
                _item_beh.add_item_for_list(tmp);
                tmp_text += ":" + tmp;
            }
        }
        //DEBUG用。ここでリストの中身を１行で表示する。
        //  Debug.Log("LOAD DATA :: "+tmp_text);


        //////////////////////////
        //全ての所持リストに対してロードでaddをかけていく
        for (int i = 0; i < 100; i++)
        {
            var tmp = PlayerPrefs.GetString("keyb" + i.ToString());
            if (tmp == "")
            {
            }
            else
            {
                _item_beh.add_item_for_bag(tmp);
            }
        }
        ///////////////////////

        //////////////////////////
        //全ての履歴リストに対してロードでaddをかけていく
        for (int i = 0; i < 100; i++)
        {
            var tmp = PlayerPrefs.GetString("keyc" + i.ToString());
            if (tmp == "")
            {
            }
            else
            {
                _item_beh.already_get_gob.Add(tmp);
            }
        }
        _item_beh.already_get_gob.Sort();

        ///////////////////////


        //player xyzはダイレクトに呼び出すだけ
        commondata.player_xyz.x= PlayerPrefs.GetFloat("player_x");
        commondata.player_xyz.y= PlayerPrefs.GetFloat("player_y");
        commondata.player_xyz.z= PlayerPrefs.GetFloat("player_z");

        //energy pointもダイレクトに読み書きする
        commondata.energy_point = PlayerPrefs.GetInt("energy_point");
    }

    //Debug用
    //saveデータの内容を確認する
    //checkボタンがおされたとき
    public void check_game_data()
    {

        var tmp_text = "";
        for (int i = 0; i < 100; i++)
        {
            var tmp = PlayerPrefs.GetString("key" + i.ToString());
            tmp_text += ":" + tmp;
        }
        tmp_text += "| item_bag -> |";

        for (int i = 0; i < 100; i++)
        {
            var tmp = PlayerPrefs.GetString("keyb" + i.ToString());
            tmp_text += ":" + tmp;
        }

        tmp_text += "| alreadyGotItem -> |";

        for (int i = 0; i < 100; i++)
        {
            var tmp = PlayerPrefs.GetString("keyc" + i.ToString());
            tmp_text += ":" + tmp;
        }

        //リストの全表示
        Debug.Log("DATA -> " + tmp_text);


        //player xyzもいけるかも
        var xx =PlayerPrefs.GetFloat("player_x");
        var yy=PlayerPrefs.GetFloat("player_y");
        var zz=PlayerPrefs.GetFloat("player_z");
        Debug.Log("XYZ -> " + xx.ToString() + "/" + yy.ToString() + "/" + zz.ToString());
    }

    //commondataをsaveする
    //  saveボタンがおされたとき
    //
    public void save_game_data()
    {
        //セーブデータは一度すべて消してからsaveしないとアペンドになってしまう様子
        break_save_data();

        var i = 0;
        foreach (string cc in _item_beh.gobname_list)
        {

            PlayerPrefs.SetString("key"+i.ToString(), cc);
            i++;
        }
        ///////////////////////////////////////////
        var j = 0;
        foreach (string cc in _item_beh.item_in_bag)
        {

            PlayerPrefs.SetString("keyb" + j.ToString(), cc);
            j++;
        }
        ///////////////////////////////////////////
        var k = 0;
        foreach (string cc in _item_beh.already_get_gob)
        {

            PlayerPrefs.SetString("keyc" + k.ToString(), cc);
            k++;
        }
        ///////////////////////////////////////////



        //player xyzもいけるかも
        PlayerPrefs.SetFloat("player_x", commondata.player_xyz.x);
        PlayerPrefs.SetFloat("player_y", commondata.player_xyz.y);
        PlayerPrefs.SetFloat("player_z", commondata.player_xyz.z);
        //
        PlayerPrefs.SetInt("energy_point", commondata.energy_point);


        PlayerPrefs.Save();
        Debug.Log("save is done.");


    }
}






