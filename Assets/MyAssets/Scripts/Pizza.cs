using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pizza : MonoBehaviour
{
    public Image PizzaUI;
    private float PizzaAmount;

    private bool PizzaTaken;

    [FMODUnity.EventRef]
    public string GrabEvent;

    // Start is called before the first frame update
    void Start()
    {
        PizzaTaken = false;
        PizzaAmount = 1.0f;
    }

    public void DecrementPizza(float decrement)
    {
        //decrease pizza
        PizzaAmount -= decrement/100;

        //update UI
        PizzaUI.fillAmount = PizzaAmount;
        
        //check end condition
        if (PizzaAmount <= 0)
        {
            AudioManager.instance.StopLevelMusic();
            GameManager.instance.TheEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PizzaTaken && collision.tag.Equals("Player"))
        {
            AudioManager.instance.setLevelMusicParam(1.0f);
            AudioManager.instance.PlaySoundOnce(SoundEvent.GrabPizza, transform.position);

            collision.GetComponent<Player_AirConsole>().AddPizza(gameObject);
            PizzaTaken = true;
        }
    }
}
