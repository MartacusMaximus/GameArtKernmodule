using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSelf : MonoBehaviour
{
    public GameObject brokenObject;

    public void Break()
    {
        GameObject broken = Instantiate(brokenObject, transform.position, transform.rotation);
        Rigidbody[] rbs = broken.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(150, transform.position, 30);
        }
        Destroy(gameObject);
    }
}
