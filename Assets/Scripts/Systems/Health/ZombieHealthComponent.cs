using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

public class ZombieHealthComponent : HealthComponent
{
    private ZombieStateMachine ZombieStateMachine;
    // Start is called before the first frame update
    void Awake()
    {
        ZombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        base.Destroy();
        ZombieStateMachine.ChanceState(ZombieStateType.Dead);
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
