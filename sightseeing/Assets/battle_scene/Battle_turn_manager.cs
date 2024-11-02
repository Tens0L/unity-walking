using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_turn_manager : MonoBehaviour
{
    private int turn_count;

    [SerializeField]
    private List<GameObject> members;


    public List<tk_hndr_manager> tks;
    private void Start()
    {
        turn_count = 0;

        members[0].transform.position = new Vector3(1f, 0.8f, 1f);
        members[1].transform.position = new Vector3(2f, 0.8f, 1f);
        members[2].transform.position = new Vector3(8f, 0.8f, 9f);
        members[3].transform.position = new Vector3(9f, 0.8f, 9f);

        Debug.Log("====START====");

        //まずこれかたづける　なぜかsendmessageでエラーでる 8/21
        //members[0].gameObject.SendMessage("generate");
        //members[0].gameObject.GetComponent<Battle_sideA_auto>().generate();

        //for (var ii=0;ii < members.Count;ii++) {
        //    members[ii].SendMessage("_generate_");
        //}

        //var _tk_ = new tk_hndr_manager();

        //turn_flow();

    }
    public int create_tk()
    {
        var tk = new tk_hndr_manager();
        tks.Add(tk);
        return tks.Count;
    }







    private void Tick_Battle_step(int _idx)
    {
        Debug.Log("tick on battle Manager");
        //これによってhuman側で仕込んだ関数が発火する
        //tick_handler?.Invoke();

        //tick_handlers[_idx]?.Invoke();

    }

    
    private void turn_flow()
    {
        //1 ターンループ
        var jj = 10;
        while (jj > 0 )
        {

            //2
            turn_count++;

            //3 playerに手番を渡す
            var _idx = turn_count % (members.Count);
            Debug.Log(_idx.ToString()+" "+members[_idx].name);

            //ここでmembers[ii]のmyturn関数が
            //発火するように仕込んでおきたい
            Tick_Battle_step(_idx);


            //members[ii].;



            //4

            //5

            //6

            //7

            //8




            jj--;
        }








    }



}
public class tk_hndr_manager
{
    public delegate void Battle_Tick();
    public Battle_Tick tick_handler;
    public void add_TickHandler()
        {

            Debug.Log("__________");


            //tick_handlers[tick_handlers.Count-1] = bt;
            //Debug.Log("tick_hdrList Recoord:"+tick_handlers.Count.ToString() );
            //tick_handlers.Count-1;
        }

}
