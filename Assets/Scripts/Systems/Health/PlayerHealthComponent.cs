using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.Invoke_OnHealthInitialized(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
