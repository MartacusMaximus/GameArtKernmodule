using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DemonCounter : MonoBehaviour
{

    private bool won = false;
    public int demonCount;
    public Text demonScore;
    public Image winScreen;
    public Text winText;

    void Start()
    {
        winScreen.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        demonCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void Update()
    {
        demonCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        demonScore.text = demonCount.ToString();

        if (demonScore != null)
        {

            if (GameObject.FindWithTag("Enemy") == null && !won)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0; won = true;
            }

            if (won)
            {
                Debug.Log("WIN BABYYYYYY");

                winScreen.gameObject.SetActive(true);
                winText.gameObject.SetActive(true);
            }
        }
    }
}
