using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCtrl : MonoBehaviour
{
    public GameObject MenuWindow;
    // Start is called before the first frame update
    void Start()
    {
        MenuWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Escape"))
        {
            MenuWindow.SetActive(true);
        }
    }
}
