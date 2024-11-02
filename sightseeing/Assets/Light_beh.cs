using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_beh : MonoBehaviour
{

    [SerializeField]
    private GameObject gm;
    private int sun_deg;
    private void Start()
    {
  

    }

    private void Awake()
    {
        //仕込む
        gm.GetComponent<GameManager>().tickHandler = tick_next_time_step;
        sun_deg = 0;
    }



    private void tick_next_time_step()
    {

        //Debug.Log("なぜか tree viewでここがはしってる問題");
        //解決したのかも


        //角度が表示が変　360のあと0度にならない。計算が変なのかも 270~+90になってる
        this.transform.Rotate(new Vector3(1f, 0f, 0f), 1f);
        sun_deg = ((int)this.transform.localRotation.eulerAngles.x);
        //Debug.Log("sun rotation:"+sun_deg.ToString());
        //gm.GetComponent<GameManager>().wall_clock_time = 24*sun_deg/360;
        if (sun_deg == 0) {
            //ここで１８時に調整したいのだけど、太陽角度取得がうまくいかないためか
            //変なタイミングでリセットはいるので、角度計算がうまくいってから
            //同期のコードを運用にのせるようにする。
            //まずはmanager側でカウントしてもらう方針で進める。
            //gm.GetComponent<GameManager>().wall_clock_time = 18f;
        }
    }



}
