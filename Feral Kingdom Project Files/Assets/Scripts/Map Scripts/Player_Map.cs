using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map : MonoBehaviour
{
    public GameObject player;
    public bool movementEnabled;
    public HUD hud;
    public float currentSpeed = 0;
    public float movementSpeed = 5f;
    public GameObject monObj;
    public MonSave monSave;

    [SerializeField]
    Vector3 pos = new Vector3();

    public void Awake()
    {
        if (GameObject.Find("MonSave(Clone)") != true)
        {
            Instantiate(monObj);
        }
        monSave = GameObject.Find("MonSave(Clone)").GetComponent<MonSave>();
        monSave.LoadFromTxtFile();
    }

    public void Start()
    {
        currentSpeed = movementSpeed;
        if (monSave.statsSaved == true)
        {
            pos.x = monSave.lastPos.x;
            pos.z = monSave.lastPos.z;
            player.transform.position = pos;
        }
        if ((pos.x == -21.5f) && (pos.z == -8.48f))
        {
            monSave.TownHeal();
        }
    }

    public void Update()
    {
        if(movementEnabled == true)
        {
            PlayerMovement();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            monSave.SaveLastMon();
            monSave.SaveParty();
            hud.ShowMainMenu();
        }
        pos.x = player.transform.position.x;
        pos.z = player.transform.position.z;
    }


    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Town"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowTown();
                movementEnabled = false;
                SavePos();
            }
        }
        else if (other.gameObject.CompareTag("Forest"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowForest();
                movementEnabled = false;
                SavePos();
            }
        }
        else if (other.gameObject.CompareTag("Swamp"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                hud.ShowSwamp();
                movementEnabled = false;
                SavePos();
            }
        }
        else if (other.gameObject.CompareTag("Mountain"))
        {
            if (Input.GetKeyDown(KeyCode.E) && movementEnabled == true)
            {
                movementEnabled = false;
                hud.ShowMount();
                SavePos();
            }
        }
    }

    public void SavePos()
    {
        monSave.SavePos(pos);
    }
}