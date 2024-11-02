using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


//ユーザの入力を受け取って反映する
//
public class Interface : MonoBehaviour
{
    private GameObject clickedGameObject;
    private LayerMask mask_item;
    private LayerMask mask_move_panel;


    private void Start()
    {
        mask_item = LayerMask.GetMask("ITEM_POP");
        mask_move_panel = LayerMask.GetMask("MOVE_PANEL");
    }

    public void OnSelect(InputAction.CallbackContext context)
    {

        //Debug.Log(context.ReadValueAsButton());


        //クリックしたときにRayをとばして
        //オブジェクトに衝突検知する

        //インターフェースにあったものを移植
        if (context.ReadValueAsButton() == false)
        {
            GameObject clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask_item)) //musk 13:CLICK
            {
                clickedGameObject = hit.collider.gameObject;
                //clickedGameObject.SendMessage("Clicked");
                clickedGameObject.TryGetComponent<ItemMarkerBeh>(out var tmp1);
                if (tmp1 is not null) {
                    tmp1.Clicked();
                }


            }
            else
            {
                return;
            }
        }
    }


}
