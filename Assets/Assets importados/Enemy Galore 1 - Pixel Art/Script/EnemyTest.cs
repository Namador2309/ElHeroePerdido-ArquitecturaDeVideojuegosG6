using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private Animator[] EnemyAnims;

    private bool IsValid(int i)
    {
        return EnemyAnims != null &&
               i < EnemyAnims.Length &&
               EnemyAnims[i] != null &&
               EnemyAnims[i].gameObject.activeSelf;
    }

    public void Animation_1_Idle()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", false);
                Debug.Log("Enemy Idle");
            }
        }
    }

    public void Animation_2_Run()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", true);
                Debug.Log("Enemy Running");
            }
        }
    }

    public void Animation_3_Hit()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Hit");
            }
        }
    }

    public void Animation_4_Death()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Death");
            }
        }
    }

    public void Animation_5_Ability()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetBool("Ability", true);
            }
        }
    }

    public void Animation_6_Attack()
    {
        for (int i = 0; i < EnemyAnims.Length; i++)
        {
            if (IsValid(i))
            {
                EnemyAnims[i].SetBool("Run", false);
                EnemyAnims[i].SetTrigger("Attack");
                Debug.Log("Enemy Attack");
            }
        }
    }
}