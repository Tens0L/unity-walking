using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_beh : MonoBehaviour
{
    private _phase myPhase;


    private enum _phase
    {
        WAIT,
        WALK,
        ATACK,
        DEFFENCE
    }

    [SerializeField]
    private GameObject battle_manager;
    private int my_idx;

    private void Start()
    {

        
    }


    public void generate()
    {

        //
        myPhase = _phase.WAIT;

        //tick_handlersリストに追加してもらったハンドラにキャラクター側の関数を仕込み、idは保持しておく
        Debug.Log("RecoordStart:" + gameObject.name);

        //
        var _id = battle_manager.GetComponent<Battle_turn_manager>().create_tk();
        //battle_manager.GetComponent<Battle_turn_manager>().tks[_id - 1].tick_handler = my_turn;


        //
        //my_idx=battle_manager.GetComponent<Battle_turn_manager>().add_TickHandler();


    }

    private void my_turn()
    {

        Debug.Log(this.gameObject.name+":turn start");
        myPhase = _phase.WAIT;


        myPhase = _phase.DEFFENCE;


        Debug.Log(this.gameObject.name + ":turn end");


    }





}
