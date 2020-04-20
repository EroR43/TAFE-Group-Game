using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map : MonoBehaviour
{
    public GameObject player;
    public bool movementEnabled;
    public HUD hud;
    public float currentSpeed = 0f;
    public float movementSpeed = 5f;
    [SerializeField]
    Vector3 pos = new Vector3();

    public void Awake()
    {
        player.transform.position = pos;
    }

    public void Start()
    {
        currentSpeed = movementSpeed;
    }

    public void Update()
    {
        if(movementEnabled == true)
        {
            PlayerMovement();
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
        MoveAnims move = player.GetComponent<MoveAnims>();
        Animator anim = player.GetComponent<Animator>();
        if (other.gameObject.CompareTag("Town"))
        {
            //if (movementEnabled == true)
            //{
            //    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            //    {
            //        move.TownForest(!anim.GetBool("moveForest"));
            //    }
            //    else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        hud.NoGo();
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                movementEnabled = false;
                hud.ShowTown();
            }
        }
        else if (other.gameObject.CompareTag("Forest"))
        {
            //if (movementEnabled == true)
            //{
            //    //    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            //    //    {
            //    //        move.ForestSwamp(!anim.GetBool("moveSwamp"));
            //    //    }
            //    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            //    {

            //        hud.NoGo();
            //    }
            //    else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        move.TownForest(!anim.GetBool("moveForest"));
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                movementEnabled = false;
                hud.ShowForest();
            }
        }
        //else if (other.gameObject.CompareTag("Swamp"))
        //{
        //    if (movementEnabled == true)
        //    {
        //        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //        {
        //            move.SwampMount(!anim.GetBool("moveMount"));
        //        }
        //        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //        {
        //            move.ForestSwamp(!anim.GetBool("moveSwamp"));
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        movementEnabled = false;
        //        hud.ShowSwamp();
        //    }
        //}
        //else if (other.gameObject.CompareTag("Mountain"))
        //{
        //    if (movementEnabled == true)
        //    {
        //        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //        {
        //            
        //        }
        //        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //        {
        //            move.SwampMount(!anim.GetBool("moveMount"));
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        movementEnabled = false;
        //        hud.ShowMount();
        //    }
        //}
    }
}
