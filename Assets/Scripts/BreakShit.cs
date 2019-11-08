using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakShit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        print("yes");

        if (collision.CompareTag("Breakable"))
        {

            if (collision.GetComponent<BreakSelf>() != null)
            {
                collision.GetComponent<BreakSelf>().Break();
            }
        }

    }
}
