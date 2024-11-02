using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_beh : MonoBehaviour
{

    private Enemy_state EnemyState;

    private enum Enemy_state
    {
        WAIT,
        WALK,
        ATACK,
        DEFFENCE
    }



    private void Clicked()
    {
        Debug.Log("clicked:" + gameObject.name);
    }

    private void Start()
    {
        EnemyState = Enemy_state.WAIT;
        Debug.Log("enemy state:"+EnemyState);
    }


    private void flow() {
        //1
        
        //2

        //3


    }



}








