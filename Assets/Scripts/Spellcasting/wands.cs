using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class wands{
    public string name;
    //public string description;
    public double dmg;
    public int id;
    public Wands(int id, string name, string description, double dmg)
    {
        this.name = name;
        this.description = description;
        this.dmg = dmg;
        this.id = id;
    }


}


