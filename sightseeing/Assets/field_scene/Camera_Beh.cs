using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Beh : MonoBehaviour
{
    [SerializeField] private GameObject player_gob;
    [SerializeField] Transform playerTr; // プレイヤーのTransform
    

    [SerializeField] private float camera_x;
    [SerializeField] private float camera_y;
    [SerializeField] private float camera_z;

    [SerializeField] private float camera_rotx;
    [SerializeField] private float camera_roty;
    [SerializeField] private float camera_rotz;

    private Camera main_cam;
    private float target_size;

    private void Start()
    {
        target_size = 5;
        switch_view1();
        main_cam = Camera.main.GetComponent<Camera>();
    }


    //ここコルーチンにしてキー入力とかをトリガーにして動かしたい
    //けどquotanionの差分とかがベクトルでかけるのか自信ないので
    //カメラだけだし一旦後回しにしておく
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            playerTr.position + new Vector3(camera_x, camera_y, camera_z), // カメラzの位置
            2.0f * Time.deltaTime);

        //ここ書き方わからん
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(camera_rotx, camera_roty, camera_rotz),
            2.0f * Time.deltaTime);

        main_cam.orthographicSize = main_cam.orthographicSize + (target_size - main_cam.orthographicSize) / 2;

    }
    //こういう感じでカメラの移動をコルーチン化してupdateなくしたい
    private IEnumerator camera_move()
    {
        int  ii = 0;
        while(ii < 100){
            yield return new WaitForSeconds(0.03f);
            ii++;
        }
    }

    private void focus_on()
    {
        //

    }

    //通常のアイソメview
    public void switch_view1()
    {

        //transform.rotation = Quaternion.Euler(32f, -27f, 0f);
        player_gob.GetComponent<UserBeh>().pl_st = commondata.PLAYER_STATE.active;

        camera_x = 7.5f;
        camera_y = 11f;
        camera_z = -14.5f;

        camera_rotx = 32f;
        camera_roty = -27f;
        camera_rotz = 0f;


        target_size = 5f;

    }

    //アイテムズーム
    public void switch_view2()
    {

        //transform.rotation = Quaternion.Euler(16f, -27f, 0f);
        player_gob.GetComponent<UserBeh>().pl_st = commondata.PLAYER_STATE.stop;
        camera_x = 7.5f;
        camera_y = 5f;
        camera_z = -14.5f;

        camera_rotx = 16f;
        camera_roty = -27f;
        camera_rotz = 0f;

        target_size = 2f;

    }

    //ズームアウト　広くみる時用
    public void switch_view3()
    {

    }


}
