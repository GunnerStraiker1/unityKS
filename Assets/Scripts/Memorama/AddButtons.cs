using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform Panel;

    [SerializeField]
    private GameObject button;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 16; i++)
        {
            GameObject newButton = Instantiate(button);
            newButton.name = "" + i;
            newButton.transform.SetParent(Panel, false);
        }
    }
}
