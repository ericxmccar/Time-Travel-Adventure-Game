using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour
{
    [SerializeField] float seconds;
    private List<Vector3> pos;
    private bool Reversing;
    private float timer;
    private bool Reversed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        pos = new List<Vector3>();
        Reversing = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reversing) {
        pos.Add(this.transform.position);
        }
        else {
            if (timer == seconds) {
                 Reversing = false;
            }
            index = pos.Count - 1;
            this.transform.position = pos[index]; 
            pos.RemoveAt(index);
            timer += Time.deltaTime;
        }
    }
    void ReverseTrue() {
        Reversing = true;
    }
}
