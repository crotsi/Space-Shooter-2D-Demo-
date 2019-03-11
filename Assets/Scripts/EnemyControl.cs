using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        scoreUITextGO = GameObject.FindGameObjectWithTag("scoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
       
        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "mermigoTag") || (col.tag == "uzayAraciTag"))
        {
            scoreUITextGO.GetComponent<GameScore>().Score += 1;
            Destroy(gameObject);
        }
    }
}
