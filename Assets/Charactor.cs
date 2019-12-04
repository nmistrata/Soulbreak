using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor{
    public static int health;
    public static int mana;
    public static int level;
    public static double exp;
    public static int wandCount = 0;
    public static int equippedWand = 0;
    public static Dictionary<int, bool> wands = new Dictionary<int, bool>();

    public static void addWand()
    {
        if(wandCount >= 3)
        {
            return;
        }

        wands.Add(++wandCount, true);
        Debug.Log(wandCount);
        equippedWand++;
    }

    public static void swapWand(int w)
    {
        if (wands[w])
        {
            equippedWand = w;
        }
    }
}

