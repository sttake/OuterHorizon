﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : ActionBase {

    override public void Action(Character chara) {
        chara.HP -= 20;
    }

}
