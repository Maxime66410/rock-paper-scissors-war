using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Scissor" || col.gameObject.tag == "Paper" || col.gameObject.tag == "Rock")
        {
            Destroy(col.gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Scissor" || other.gameObject.tag == "Paper" || other.gameObject.tag == "Rock")
        {
            Destroy(other.gameObject);
        }
    }
}
