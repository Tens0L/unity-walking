using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System.Threading.Tasks;//for async


public class FieldMapManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> map_panels;

    private GameObject[,,] map_panels_generated; 

    private Vector3 player_vector3;
    void Awake()
    {
        map_panels_generated = new GameObject[100,100,100];

        generate_panel_to_list();

        //playerの座標を問い合わせて
        //その座標のときに呼び出すパネルを決めておけばOKのはず
        //まずはpanelに座標の名前をいれておいて
        //呼び出す時にその座標をたよりに呼び出すという方策から試す

        //playerの位置情報初期化
        //player_vector3 = new Vector3(0f, 0f, 0f);

        //fieldのpanelの表示初期化
        //all_panel_off();
        update_panel();
    }


    public void raise_update_panel(Vector3 v3)
    {
        player_vector3 = v3;

        //まさかのtask runがうまく動かない原因になっていた
        //ひとまず普通の methodに切り替えて他を進める。
        //Task.Run(update_panel);
        update_panel();
    }


    void all_panel_off()
    {
        //Debug.Log("all panel off");
        foreach (GameObject gob in map_panels)
        {

            gob.SetActive(false);
            
        }
    }

    void generate_panel_to_list()
    {
        //インスペクタでアタッチしたlistのままだと
        //呼び出しが難しいので、３次元の配列に整理しておく


        foreach(GameObject gob in map_panels)
        {
            //パネルの座標をつかって配列にいれていく
            var p_x = gob.transform.position.x;
            var p_y = gob.transform.position.y;
            var p_z = gob.transform.position.z;

            map_panels_generated[((int)(p_x/10)), ((int)(p_y/10)), ((int)(p_z/10))]=gob;

        }
    }




    //こういう感じのmethodを走らせたい　頻度は低くていい 非同期処理使ってみたい
    //async あとまわし
    void update_panel()
    {
        all_panel_off();
        //Debug.Log("panel on");
        float tmp_x;
        float tmp_y;
        float tmp_z;

        tmp_x = player_vector3.x;
        tmp_y = player_vector3.y;
        tmp_z = player_vector3.z;


        //せっかくなまえから配列検索できるのだから
        //整理しておけば使いやすいはず

        //登録済みの配列からよびだすだけ
        if (map_panels_generated[((int)(tmp_x / 10)), ((int)(tmp_y / 10)), ((int)(tmp_z / 10))] is not null)
        {
            map_panels_generated[((int)(tmp_x / 10)), ((int)(tmp_y / 10)), ((int)(tmp_z / 10))].SetActive(true);
        }
        
        //Debug.Log( "_" + (((int)tmp_x)*10).ToString() + "_" + (((int)tmp_y)*10).ToString() + "_" + (((int)tmp_z)*10).ToString() );
        //foreach (GameObject gob in map_panels)
        //{

        //    //周囲１マス分も表示するようにする
        //    if (gob.name.Contains(generate_panel_name(tmp_x + 0, tmp_y + 0, tmp_z + 0 ))) { gob.SetActive(true); }
        //    if (gob.name.Contains(generate_panel_name(tmp_x + 10, tmp_y + 0, tmp_z + 0 ))) { gob.SetActive(true); }

        //    if (gob.name.Contains(generate_panel_name(tmp_x + 0, tmp_y + 0, tmp_z + 10 ))) { gob.SetActive(true); }
        //    if (gob.name.Contains(generate_panel_name(tmp_x - 10, tmp_y + 0, tmp_z + 0 ))) { gob.SetActive(true); }

        //    if (gob.name.Contains(generate_panel_name(tmp_x + 0, tmp_y + 0, tmp_z - 10 ))) { gob.SetActive(true); }
        //    if (gob.name.Contains(generate_panel_name(tmp_x + 10, tmp_y + 0, tmp_z + 10 ))) { gob.SetActive(true); }

        //    if (gob.name.Contains(generate_panel_name(tmp_x + 10, tmp_y + 0, tmp_z - 10 ))) { gob.SetActive(true); }
        //    if (gob.name.Contains(generate_panel_name(tmp_x - 10, tmp_y + 0, tmp_z + 10 ))) { gob.SetActive(true); }
        //    if (gob.name.Contains(generate_panel_name(tmp_x - 10, tmp_y + 0, tmp_z - 10 ))) { gob.SetActive(true); }


        //}

        //ここでbakeかけてもいいかも
        //いや、動的bakeはエラーのもとだからできればbakeはあらかじめしておきたい。
        //そもそもbakeして保存しておくだけなら負荷かからないし

    }





    //panaelの名前をXYZ値から生成するmethod
    //パネルを操作する時につかう
    //つかわない
    private string generate_panel_name(float tmp_x, float tmp_y,float tmp_z)
    {
        //つかわない
        return "_" + (((int)tmp_x) * 10).ToString() + "_" + (((int)tmp_y) * 10).ToString() + "_" + (((int)tmp_z) * 10).ToString();
    }


}
