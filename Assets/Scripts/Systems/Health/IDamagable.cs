using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Health
{
    public interface IDamagable
    {
        void TakeDamage(float damage);

        void Destroy();
    }
}

