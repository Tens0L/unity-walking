using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//ユーザの入力を受け取って反映する
public class UserBeh : MonoBehaviour
{
    NavMeshAgent agent;

    //アニメーション制御のためのオブジェクト
    [SerializeField]
    private Animator anim_con;

    //energy_pointを可視化するためのスライダー
    private Slider energy_point_slider;

    private float walk_speed;
    public  int energy_point;

    //count tick
    private int step_count;

    public commondata.PLAYER_STATE pl_st;


    //ゲームオーバーをコールバックで実装するため
    public delegate void OnCompleteDelegate(string result);
    public OnCompleteDelegate deathHandler;
    //game managerへコールバックする
    public void death()
    {
        //この関数が走ったことでgameManager側で仕込んだ関数が発火する
        Debug.Log("exe myproc. run in userBeh.csh");
        deathHandler?.Invoke("run in UserBeh");
    }

    //next time stepで実装する
    public delegate void NextTimeStepDelegate(int timestep);
    public NextTimeStepDelegate nextHandler;
    //game managerへコールバックする
    public void next_timestep()
    {
        //この関数が走ったことでgamemanager側で仕込んだ関数が発火する
        //Debug.Log("next time step. run in UserBeh.csh");
        nextHandler?.Invoke(step_count);
    }



    private void Start()
    {
        step_count = 0;
         pl_st= commondata.PLAYER_STATE.active;

        energy_point_slider = GetComponentInChildren<Slider>();

        agent = GetComponent<NavMeshAgent>();
        //saveされている場所へ瞬間移動
        //一度agentをOFFにしてから移動する。インスペクター上でもOFFにしてある。
        agent.enabled = false;
        gameObject.transform.position = commondata.player_xyz;
        agent.enabled = true;


        energy_point = commondata.energy_point;
        energy_point_slider.value = energy_point;

        walk_speed = 1f;//ここデータベースから呼び出したい あとでv

        //フィールドマップマネージャーをfindしてpanel_updateをコールする
        GameObject.Find("scManager").GetComponent<FieldMapManager>().raise_update_panel(gameObject.transform.position);

    }

    //プレイヤーのHUDに表示する数値やステータスバーの更新
    public void update_player_HUD() {
        energy_point_slider.value = energy_point;
    }

    //playerの位置情報を取得して一時保存
    public void Save_player_xyz()
    {
        commondata.player_xyz = gameObject.transform.position;
        commondata.energy_point = energy_point;
    }


    // playerにinput systemが設定されていて
    //そこから制御している
    //
    public delegate void OnPressTriggerHandler();
    public OnPressTriggerHandler OnPrHandler;
    public void OnHoldtest(InputAction.CallbackContext context)
    {
        //flagかなんかで管理して、ずっとループするようにする
        if (true)
        {
            OnPrHandler?.Invoke();
            energy_point -= 10;
            step_count += 1;
            next_timestep();
            if (energy_point <= 0){death();}
            var v2 = context.ReadValue<Vector2>();
            gameObject.transform.Translate(new Vector3(v2.x, 0, v2.y) * walk_speed);
            if (context.ReadValue<Vector2>().magnitude < 0.01){anim_con.SetBool("isWalk", false);}
            else{anim_con.SetBool("isWalk", true);}
            GameObject.Find("scManager").GetComponent<FieldMapManager>().raise_update_panel(gameObject.transform.position);
            update_player_HUD();
        }
    }


    public void ___OnPress(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        
        //状態がSTOPでなければ動くようにしていた
        //if (pl_st != commondata.PLAYER_STATE.stop)
        Debug.Log("state is "+pl_st);
        //Hold実験のため一旦解除
        if (true)
        {

            //OnPressが走ったら発火するd
            OnPrHandler?.Invoke();

            //Debug.Log(context.ReadValue<Vector2>().magnitude);
            //行動したのでポイントを減らす
            energy_point -= 10;
            step_count += 1;
            next_timestep();

            if (energy_point <= 0)
            {
                //ポイントがゼロになったら死亡メソッドを呼び出す
                death();
            }
            //Debug.Log(energy_point);

            var v2 = context.ReadValue<Vector2>();
            //translateつかうと動きがカクカクする
            gameObject.transform.Translate(new Vector3(v2.x, 0, v2.y) * walk_speed);


            if (context.ReadValue<Vector2>().magnitude < 0.01)
            {
                anim_con.SetBool("isWalk", false);

            }
            else
            {
                anim_con.SetBool("isWalk", true);
            }


            //agentつかうと動きがもっさりする
            //for (int i = 0; i < 30; i++)
            //{
            //agent.destination = gameObject.transform.position + (new Vector3(v2.x, 0, v2.y) * walk_speed );
            //}


            //カーソル上下左右と画面の上下左右をあわせるならこれ
            //agent.Move(new Vector3(v2.x + -v2.y, 0, v2.x + v2.y) * 0.5f); //これだと上下左右が画面とそろう


            //というかコントローラ作るのがいいのかもしれない
            //}//ここしらべるagentのきのうで一発でうごけるやつればよし、なければつくる7/10
            //animationで動かすのもありか？


            //フィールドマップマネージャーをfindしてpanel_updateをコールする
            GameObject.Find("scManager").GetComponent<FieldMapManager>().raise_update_panel(gameObject.transform.position);

            //player用のHeadUpDisplayをここで更新するようにしてみる
            update_player_HUD();

        }
    }




}
