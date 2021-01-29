using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public TargetCondition targetCondition;
    public Text child;

    // test display
    private Text zoiText;
    private Text worryText;

    public float speed;

    private int requireTypeZoi;
    private int requireTypeWorry;

    private int hitByZoi;
    private int hitByWorry;

    private void Start()
    {
        zoiText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        worryText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponentInChildren<Text>();

        requireTypeZoi = targetCondition.RequireTypeZoi;
        requireTypeWorry = targetCondition.RequireTypeWorry;

        

        zoiText.text = requireTypeZoi.ToString();
        worryText.text = requireTypeWorry.ToString();

        if (requireTypeZoi == 0) transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        if (requireTypeWorry == 0) transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    }


    private void Update()
    {
        transform.Translate(0, speed, 0);

        if (requireTypeZoi - hitByZoi <= 0) transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        if (requireTypeWorry - hitByWorry <= 0) transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        if (collision.GetComponent<Collider2D>().tag == "Player")
        {

            if (!collision.GetComponent<Sushi>().hasHit)
            {
                var bulletName = collision.GetComponent<Sushi>().Name;

                int number;

                if (bulletName == "Zoi")
                {
                    int.TryParse(zoiText.text, out number);

                    hitByZoi++;
                    number--;
                    zoiText.text = number.ToString();
                }
                else if (bulletName == "Worry")
                {
                    int.TryParse(worryText.text, out number);

                    hitByWorry++;
                    number--;
                    worryText.text = number.ToString();
                }
                StartCoroutine(collision.GetComponent<Sushi>().Hit());
            }
            
        }
        
        else if (collision.GetComponent<Collider2D>().tag == "DamageZone")
        {
            Debug.Log("Player damaged");
            gameObject.SetActive(false);

        }

        if (requireTypeWorry - hitByWorry <= 0 && requireTypeZoi - hitByZoi <= 0) gameObject.SetActive(false);
    }
}
