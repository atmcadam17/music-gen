using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bass : Synthesizer
{
    [SerializeField] private float holdChance = .3f;
    
    new void Start() {
        base.Start();
    }
}
