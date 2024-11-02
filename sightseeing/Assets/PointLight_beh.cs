using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight_beh : MonoBehaviour
{
    [SerializeField]
    private GameObject gm;

    [SerializeField]
    private float toggle_on_at;
    [SerializeField]
    private float toggle_off_at;
    [SerializeField]
    private float light_intensity;

    private Light point_light;

    private void Start()
    {
        //仕込む ハンドラにうわがきするかたちなので、これはだめみたい
        //ハンドラ１つに複数発火するようにするにはどうすればいいのか
        //gm.GetComponent<GameManager>().tickHandler=check_and_toggle_Light;

        //こっち側からハンドラを追加するようなことができればいい

        //例えばgmのリストにthis-gameObjをaddして
        //tickのときに全てにbroadcastMESSAGEしてもらうようにする
        //そのときにget_tickを必ず用意する必要あるので
        //Monobehaviourをオーバーロードして作っておくといいかも


        //Debug.Log("point light add to-> gm.tick_recievwe[List]");
        gm.GetComponent<GameManager>().tick_reciever.Add(this.gameObject);
        point_light = GetComponent<Light>();

    }

    //messageで受け取る
    public void tick_timestep()
    {
        check_and_toggle_Light();
    }


    private void check_and_toggle_Light()
    {
        var now = gm.GetComponent<GameManager>().wall_clock_time;
        if (toggle_on_at < toggle_off_at)
        {
            if ((toggle_on_at < now) && (now < toggle_off_at))
            {
                point_light.intensity = light_intensity;
            }
            else
            {
                point_light.intensity = 0f;
            }
        }
        else
        {
            if ((toggle_on_at < now) || (now < toggle_off_at))
            {
                point_light.intensity = light_intensity;
            }
            else
            {
                point_light.intensity = 0f;
            }
        }
    }
}
