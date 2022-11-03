using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public UnityEngine.UI.Text GoldCountText;
    public UnityEngine.UI.Text StonesCountText;
    public UnityEngine.UI.Text WoodCountText;

    public UnityEngine.UI.Text MessageText;

    private struct Message
    {
        public string Text;
        public Color Color;
        public int Seconds;

        public Message(string text, Color color, int seconds)
        {
            Text = text;
            Color = color;
            Seconds = seconds;
        }
    }
    private Queue<Message> messagesQueue = new Queue<Message>();
    private bool messageShowing = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GoldCountText.text = "Gold: " + World.MainCamera.GetComponent<World>().Gold;
        StonesCountText.text = "Stones: " + World.MainCamera.GetComponent<World>().Stones;
        WoodCountText.text = "Wood: " + World.MainCamera.GetComponent<World>().Wood;

        StartCoroutine(ShowNextMessage());
    }

    public void AddMessageToShow(string text, Color color, int seconds)
    {
        messagesQueue.Enqueue(new Message(text, color, seconds));
    }

    private IEnumerator ShowNextMessage()
    {
        if (messageShowing) yield break;

        messageShowing = true;

        if (messagesQueue.Count > 0)
        {
            Message m = messagesQueue.Dequeue();

            MessageText.text = m.Text;
            MessageText.color = m.Color;

            yield return new WaitForSeconds(m.Seconds);

            MessageText.text = "";

            yield return new WaitForSeconds(0.5f);
        }

        messageShowing = false;
    }
}
