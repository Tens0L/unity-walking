using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class Player_Chase : MonoBehaviour
{

    [SerializeField]
    private GameObject target_gob;

    //agent完全につぶした。userBehのほうは移動範囲の定義をしているので、現状ではONのまままにしておく10/15

    //[SerializeField]
    //private NavMeshAgent agent;

    private float player_speed;


    private void Start()
    {

        //agent = GetComponent<NavMeshAgent>();
        //saveされている場所へ瞬間移動
        //一度agentをOFFにしてから移動する。インスペクター上でもOFFにしてある。
        //agent.enabled = false;
        gameObject.transform.position = commondata.player_xyz;

        player_speed = 2f;

        target_gob.GetComponentInParent<UserBeh>().OnPrHandler = go_to_xyz_setting;

    }

    private void go_to_xyz_setting()
    {
        //Debug.Log(" go to xyz setting");
        StartCoroutine("go_to_xyz");
    }


    private IEnumerator go_to_xyz()
    {
        //Debug.Log("ienumerator go to xyz");
        
        Vector3 target_position = target_gob.transform.position;
        Vector3 dir_to_target = (target_gob.transform.position - this.gameObject.transform.position);


        target_position.y = this.gameObject.transform.position.y;
        transform.LookAt(target_position);

        while (dir_to_target.sqrMagnitude > 0.1f)
        {
            //
            target_position.y = this.gameObject.transform.position.y;
            //
            transform.position = Vector3.MoveTowards(
                this.gameObject.transform.position,
                target_gob.transform.position,
                0.1f
                );
            //

            //transform.Translate(dir_to_target*0.5f);
            dir_to_target = (target_gob.transform.position - this.gameObject.transform.position);

            yield return new WaitForSeconds(0.03f);



        }

    }


}
