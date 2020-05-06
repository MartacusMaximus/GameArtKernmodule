using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string levelName;

    private void OnCollisionEnter(Collision collision)
    {
        print("Hell");
        SceneManager.LoadScene(levelName);

    }
}
