using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;
    public GameObject mermi;
    public GameObject mermiPozisyon1;
    public GameObject mermiPozisyon2;
    public GameObject mermiPozisyon3;
    //reference to the lives ui text
    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;
    //float maxSpeed = 5f;
    public void Init()
    {
        lives = MaxLives;
        //update lives ui text
        LivesUIText.text = lives.ToString();
        transform.position = new Vector2(-5, 0);
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Space ile ateş etmek
        if (Input.GetKeyDown("space"))
        {
            // uçağın burnu
            GameObject mermi1 = (GameObject)Instantiate(mermi);
            mermi1.transform.position = mermiPozisyon1.transform.position;
            // Kanatlar
            /* 
             GameObject mermi2 = (GameObject)Instantiate(mermiGO);
             mermi2.transform.position = mermiPozisyon2.transform.position;

             GameObject mermi3 = (GameObject)Instantiate(mermiGO);
             mermi3.transform.position = mermiPozisyon3.transform.position;

             */
        }
        //float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float x = 0f;

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);

        /*
         Vector3 pos = transform.position;

         pos.y += Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;


         transform.position = pos;*/


    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.y = max.y - 0.7f;
        min.y = min.y + 2.4f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;


        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "canavarTag")
        {
            lives--;
            LivesUIText.text = lives.ToString();
            if(lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            } 
        }
    }
}
