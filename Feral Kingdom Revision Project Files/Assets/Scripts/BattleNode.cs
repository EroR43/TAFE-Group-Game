using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleNode : MonoBehaviour
{
    public int battleIndex;

    [SerializeField]
    private bool completed = false;

    private Fader fader;
    private Animator anim;

    private void Awake()
    {
        fader = GetComponent<Fader>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (GameManager.battleNodeTable.ContainsKey(battleIndex) == false)
        {
            GameManager.battleNodeTable.Add(battleIndex, completed);
        }
        else
        {
            completed = (bool)GameManager.battleNodeTable[battleIndex];
        }
    }

    /// <summary>
    /// Returns false if battle has been completed.
    /// </summary>
    /// <returns></returns>
    public bool GoToBattle()
    {
        if (completed == false && GameManager.instance.GetMonster().data.GetCurrHP() > 0)
        {
            GameManager.currBattle = battleIndex;
            SceneManager.LoadScene(battleIndex);
            return true;
        }
        else
        {
            return false;
        }
    }


    public void CompleteBattle()
    {
        completed = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && battleIndex > 1)
            {
                //Debug.Log("Triggered");
                GoToBattle();
            }
            //else
            //{
            //    Debug.Log("Triggered 2");
            //}
        }
    }

    public void OnTriggerExit(Collider other)
    {
        fader.Fade(!anim.GetBool("inTrigger"));
    }
}
