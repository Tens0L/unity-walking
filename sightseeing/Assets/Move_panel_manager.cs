using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_panel_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject player_gob;


    [SerializeField]
    private GameObject panel_gob;





    private void Start()
    {
        this.gameObject.transform.position = commondata.player_xyz;
        var panel_list=new List<Vector3>();

        var d_xyz_0 = new Vector3(0f, 0, 0);

        //panel_list.Add(this.gameObject.transform.position);
        //List<Vector3> panel_list = new List<Vector3>();
        put_panel(this.gameObject,d_xyz_0,8,panel_list);

    }

    private int put_panel(GameObject parent,Vector3 _xyz,int _count,List<Vector3> _pl)
    {

        //xyz
        var _p = Instantiate(panel_gob,parent.transform);
        _p.transform.Translate(_xyz);
        

        if (_pl.Contains(_p.transform.position))
        {
            Destroy(_p);
        }
        else
        {

            _pl.Add(_p.transform.position);
            //count--
            _count--;

            if (_count > 0)
            {

                Debug.Log("----" + _count.ToString() + " " + _xyz.ToString());

                //なぜかマイナスとプラスで同時に設置しようとするとおかしな図形になってしまう。countのせいかlistのせいか

                var d_xyz_1 = new Vector3(1f, 0, 0);
                put_panel(_p, d_xyz_1, _count,_pl);

                var d_xyz_3 = new Vector3(0, 0, 1f);
                put_panel(_p, d_xyz_3, _count,_pl);



                //var d_xyz_2 = new Vector3(-1f, 0, 0);
                //put_panel(_p, d_xyz_2, _count,_pl);

                //var d_xyz_4 = new Vector3(0, 0, -1f);
                //put_panel(_p, d_xyz_4, _count, _pl);

                return 0;
            }
        }
        return 0;
    }


}
