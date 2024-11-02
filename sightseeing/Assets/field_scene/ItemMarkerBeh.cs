using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//  itemのマーカー側
//
public class ItemMarkerBeh : MonoBehaviour
{

    public GameObject parent_gob;
    //クリックされたらparentにクリックされたことを通知するだけ
    public void Clicked()
    {
        parent_gob.SendMessage("Active_Display");

        //このタイミングでcameraにアクセスして
        //viewを切り替えてもとにもどすイベントをしこんでおく





    }


}
