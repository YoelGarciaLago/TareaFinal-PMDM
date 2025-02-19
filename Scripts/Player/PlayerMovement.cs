using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float movementY;
    int count = 0;
    private float movementX;
    public TextMeshProUGUI countText;
    public float speed = 0;
    private Animator anim;
    private Renderer colores;
    private int level = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        anim = GetComponent<Animator>();
        colores = GetComponent<Renderer>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementY = movementVector.y;
        movementX = movementVector.x;
    }

    private void FixedUpdate()
    {
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //rb.AddForce(movement * speed);

        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        transform.Translate(dir * speed, Space.World);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        string currentStateName = stateInfo.IsName("Quieto") ? "Quieto" :
          stateInfo.IsName("Moviendose") ? "Moviendose" :
          stateInfo.IsName("Volando") ? "Volando" :
          "Estado Desconocido";

         if (IsAtCertainHeight())
        {
            anim.SetBool("volando", true);
            return;
        }
        if (IsMoving())
        {
            anim.SetBool("volando", false);
            anim.SetBool("moverse", true);
        }
        else if (IsIdle())
        {
            anim.SetBool("volando", false);
            anim.SetBool("moverse", false); // Estado "quieto"
        }
        currentActionState(currentStateName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            if (count >= 5)
            {
                GameObject muro = GameObject.FindGameObjectWithTag("Muro");
                if (muro != null)
                {
                    Collider targetCollider = muro.GetComponent<Collider>();
                    if (targetCollider != null)
                    {
                        targetCollider.enabled = false;
                    }
                }
                //muro.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "You Lose";
        }
        if (other.gameObject.CompareTag("Enemy2"))
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "You Lose";
        }
        if (other.gameObject.CompareTag("Suelo"))
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "You're dead :/";
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 9)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemysLvl2 = GameObject.FindGameObjectsWithTag("Enemy2");
            foreach (GameObject wazaaa in enemys)
            {
                UnityEngine.AI.NavMeshAgent agent = wazaaa.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null)
                {
                    agent.enabled = false; 
                }
            }
            level++;
            if (level == 2)
            {
                count = 0;
                countText.text = "Count: " + count.ToString();
                transform.position = new Vector3(449.65f, 1.667f, -47.43f);
                foreach (var enemy in enemysLvl2)
                {
                    enemy.SetActive(true);
                    UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.enabled = true; 
                    }
                }
                
            }
            else
            {
                countText.text = "You Win";
                foreach (var enemy in enemys)
                {
                    Destroy(enemy);
                }
                foreach (var enemy in enemysLvl2)
                {
                    Destroy(enemy);
                }
            }
        }
    }
    bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f; // 0.1f para evitar pequeñas variaciones
    }

    bool IsIdle()
    {
        return rb.velocity.magnitude < 0.1f && !IsMoving();
    }
    public bool IsAtCertainHeight()
    {
        return transform.position.y >= 0.6f;
    }
    void currentActionState(string currentState)
    {
        switch (currentState)
        {
            case "Quieto":
                colores.material.color = Color.red;
                break;
            case "Moviendose":
                colores.material.color = Color.blue;
                break;
            case "Volando":
                colores.material.color = Color.green;
                break;
        }
    }
}
