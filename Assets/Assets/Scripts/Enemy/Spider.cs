using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField]
    private GameObject _acidPrefab;
    public int Health { get; set; }
    // Initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        //
    }
    public void Damage()
    {
        if (isDead == true)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
    public override void Movement()
    {
        //sit still
    }
    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}
