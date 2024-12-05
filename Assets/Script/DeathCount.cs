using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathCount 
{
    static int Deathcnt = 0;
    public static void CountUp() {  Deathcnt++; }
    public static int GetCount() { return Deathcnt; }
}
