using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spellshower : MonoBehaviour {
    public Text txt;
    WandData w;
    // Use this for initialization
    void Start () {
        w = new WandData();
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            if (Charactor.wandCount > Charactor.equippedWand)
            {
                Charactor.equippedWand++;
            }
            else if (Charactor.wandCount == Charactor.equippedWand)
            {
                Charactor.equippedWand = 0;
            }
            txt.text = w.wd[Charactor.equippedWand].name;
  
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (Charactor.equippedWand > 0)
            {
                Charactor.equippedWand--;
            }
            else if (Charactor.equippedWand == 0)
            {
                Charactor.equippedWand = Charactor.wandCount;
            }
            txt.text = w.wd[Charactor.equippedWand].name;
        }
    }
}
