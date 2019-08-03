using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLeaf : ActionBase {

    override public void Action(Character chara) {
        chara.AddHP(20f);
    }

}
