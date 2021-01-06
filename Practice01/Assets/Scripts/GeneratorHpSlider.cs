using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorHpSlider : MonoBehaviour
{

    public Slider HpSlider;
    // Start is called before the first frame update
    void Start()
    {
        var slider = Resources.Load("Prefabs/HpSlider");
        var hpSlider = Instantiate(slider, GameObject.Find("Canvas").transform) as GameObject;
        hpSlider.GetComponent<FollowCreature>().FollowTarget = transform;
        HpSlider = hpSlider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
