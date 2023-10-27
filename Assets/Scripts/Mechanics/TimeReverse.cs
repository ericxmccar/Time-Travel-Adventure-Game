using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverse : MonoBehaviour
{
    [SerializeField] int posSize;
    private List<Vector3> pos;
    private bool Reversing;
    private bool Reversed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        pos = new List<Vector3>();
        Reversing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reversing) {
            pos.Add(this.transform.position);
            if (pos.Count > posSize) pos.RemoveAt(0);
        }
        else {
            if (pos.Count > 0)
            {
                this.transform.position = pos[pos.Count - 1];
                pos.RemoveAt(pos.Count - 1);
            }
            else
            {
                Reversing = false;
            }
        }
    }
    public void ReverseTrue() {
        Reversing = true;
    }
}
