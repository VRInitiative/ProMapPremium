using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scriptenable : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.MoveGameObjectToScene(GameObject.Find("Headjack Camera"), SceneManager.GetActiveScene());
        Destroy(GameObject.Find("Headjack Camera"));

        SceneManager.MoveGameObjectToScene(GameObject.Find("Cinema"), SceneManager.GetActiveScene());
        Destroy(GameObject.Find("Cinema"));

        SceneManager.MoveGameObjectToScene(GameObject.Find("OVRManager"), SceneManager.GetActiveScene());
        Destroy(GameObject.Find("OVRManager"));
    }


}
