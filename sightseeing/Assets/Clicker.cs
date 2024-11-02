using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    private GameObject clickedGameObject;
    private LayerMask mask;





    private void Start()
    {
        mask = LayerMask.GetMask("CLICK");

    }




    public void OnSelect(InputAction.CallbackContext context)
    {
        //Debug.Log("click");


        if (context.ReadValueAsButton() == false)
        {
            GameObject clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                clickedGameObject = hit.collider.gameObject;

                //clickedGameObject.SendMessage("Clicked");

                clickedGameObject.TryGetComponent<ItemMarkerBeh>(out var tmp1);
                if(tmp1 is not null) { tmp1.Clicked();}

                //Debug.Log(clickedGameObject.name);
            }
            else
            {
                return;
            }
        }
    }

}
