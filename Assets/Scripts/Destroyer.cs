using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string targetTag;
    public bool selfDestruction; // enable self destruction
    public bool selfDestructOnTop;
    public float selfDestructPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selfDestruction)
        {
            if (selfDestructOnTop)
            {
                if (transform.position.y > selfDestructPos) Destroy(this.gameObject);
            }
            else
            {
                if (transform.position.y < selfDestructPos) Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == targetTag)
        {
            if(targetTag == "monster")
            {
                collision.gameObject.GetComponent<MonsterStrategy>().Scoring();
                collision.gameObject.SetActive(false);
                Destroy(this.gameObject);
                collision.gameObject.GetComponentInParent<GameplayManager>().RemoveChildIndex(collision.transform.GetSiblingIndex());
            }

            if(targetTag == "rocket")
            {
                if(selfDestruction) Destroy(this.gameObject);
                collision.transform.GetComponent<RocketController>().Destroyed();
            }
        }
    }
}
