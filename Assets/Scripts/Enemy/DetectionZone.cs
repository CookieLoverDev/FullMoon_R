using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public Collider2D detcZone;
    public List<Collider2D> detectedObjcs = new List<Collider2D>();
    public string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            detectedObjcs.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            detectedObjcs.Remove(collision);
        }
    }
}
