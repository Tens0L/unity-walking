using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAreaBeh : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Item")
        {
            //ここでチェックするのは
            //アイテムマーカーがおふにならないみたいなのでチェックする
            //


            //other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.SendMessage("Activate_Marker");

        }
    }



    // ここなぜかはなれたときにTriggerExitを通らない問題
    //　TriggerEnterは通っているので、いまの移動方法が悪いわけではない
    //　なぜ？
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Item")
        {

            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.SendMessage("InActivate_Marker");

        }
    }

}
