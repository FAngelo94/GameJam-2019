using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public RectTransform PizzaPanel;
    public float PercentOfSecond=1;

    private bool PizzaTaken;

    private bool endMusic;

    private float Width;
    private float BoxRemain;

    [FMODUnity.EventRef]
    public string GrabEvent;

    public GameObject MusicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PizzaTaken = false;
        endMusic = true;
        Width = 1;
        BoxRemain = 100;
    }

    public void DecrementPizza()
    {
        BoxRemain -= 0.1f;
        //        Debug.Log(PizzaPanel.localScale);
        PizzaPanel.localScale = new Vector2(1 - Width / 100 * BoxRemain, 1);

        if (BoxRemain <= 3 && endMusic)
        {
            endMusic = false;
            MusicPlayer.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("End", 1);
        }
        if (BoxRemain <= 0)
            GameManager.instance.TheEnd();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PizzaTaken && collision.tag.Equals("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(GrabEvent, transform.position);

            MusicPlayer.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("Pizza_Taken", 1);

            collision.GetComponent<Player>().AddPizza(gameObject);
            PizzaTaken = true;
        }
    }
}
