using GameAPI.Async.Generic;
using GameAPI.Tasks;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Typewriter : TaskedBehaviour<Typewriter>
{
    public float Speed = 1f;

    private string content;
    private TextMeshProUGUI text;

    public void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        content = text.text;

        text.text = "";
    }

    // public void Update()
    // {
    //     Debug.Log(CurrentTask);
    // }

    public void Trigger()
    {
        foreach (char c in content)
            Queue(new TypeTask(text, c.ToString(), 1f / Speed));
    }

    public void Stop() =>
        ClearTasks();

    public void Reset()
    {
        Stop();
        text.text = "";
    }
        

    public class TypeTask : GameTask<Typewriter>
    {
        private readonly TextMeshProUGUI component;
        private readonly string content;
        private readonly float delay;

        public TypeTask(TextMeshProUGUI component, string content, float delay)
        {
            this.component = component;
            this.content = content;
            this.delay = delay;
        }

        protected override async Task Run()
        {
            Debug.Log("Adding -> " + content);

            component.text += content;
            await new Delay(delay);
        }
    }
}