using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandData
{
    public List<Wands> wd = new List<Wands>();


    public WandData()
    {
        Wands w1 = new Wands(0, "Big Wand", "Big wand :)", 10);

        Wands w2 = new Wands(1, "Massive wand", "bigger than big wand ::))", 100);

        Wands w3 = new Wands(2, "Gigantic Wand", "bigger than the large massive wang", 1000);

        Wands w4 = new Wands(3, "Enormous Wand", "larger than the all other wands", 10000);

        wd.Add(w1);
        wd.Add(w2);
        wd.Add(w3);
        wd.Add(w4);
    }

}