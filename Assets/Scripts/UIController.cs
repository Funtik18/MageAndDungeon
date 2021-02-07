using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [HideInInspector] public static UIController instance;

    public Text hpCount;
    public Text moneyCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void moneyChange(int count)
    {
        moneyCount.text= count.ToString();
    }

    public void hpChange(int count)
    {
        hpCount.text= count.ToString();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
