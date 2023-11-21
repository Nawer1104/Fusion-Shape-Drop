using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    int ID;
    public GameObject vfxMerge;
    bool canCheck = false;
    public Color color;
    Vector3 startPos;
    public bool isFalled = false;

    void Start()
    {
        ID = GetInstanceID();
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canCheck = true;
        if (collision.gameObject.CompareTag("Block"))
        {
            if (ID < collision.gameObject.GetComponent<Merge>().ID) { return; }
            if (collision.gameObject.GetComponent<Merge>().color == color)
            {
                GameObject vfx = Instantiate(vfxMerge, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("GreenZone") && !isFalled)
        {
            transform.position = startPos;
            canCheck = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("RedZone") && isFalled && canCheck)
        {
            GameManager.Instance.ResetGame();
        }
    }
}