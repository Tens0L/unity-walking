using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneBeh : MonoBehaviour
{
    //private Scene tmp;
    private void Start()
    {
        //var tmp = SceneManager.GetSceneByName("field");
        //var tmp = SceneManager.GetSceneByName("tree");
        //var tmp = SceneManager.GetActiveScene();

        //SceneManager.LoadSceneAsync("field");
        //SceneManager.UnloadSceneAsync("field");




    }

    //callc from button
    public void change_scene_field(){

        SceneManager.LoadScene("field");
        //SceneManager.LoadSceneAsync("field");
        //SceneManager.UnloadSceneAsync("tree");

    }

    public void change_scene_tree(){
        SceneManager.LoadScene("tree");
        //SceneManager.LoadSceneAsync("tree");
        //SceneManager.UnloadSceneAsync("field");


        //SceneManager.LoadScene("tree",LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync("field");


        //SceneManager.SetActiveScene(tmp);

    }

    public void change_scene_title(){

        SceneManager.LoadScene("TitleMenu");
        //SceneManager.UnloadSceneAsync("tree");


    }

    public void change_scene_End()
    {
        SceneManager.LoadScene("clear_game");
    }

    public void change_scene_Over()
    {
        SceneManager.LoadScene("game_over");
    }
}
