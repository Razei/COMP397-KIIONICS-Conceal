using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,
        RUN,
        JUMP,
        KICK
    }

    private Animator animator;
    public CharacterController controller;
    public AudioSource hitSound;
    public bool invisible;

    [Header("Inventory")]
    public InventoryObject inventory;

    [Header("Abilities")]
    public float invisibilityDuration;

    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;

    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public bool isGrounded;
    public LayerMask groundMask;
    public Vector3 velocity;
   
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("AnimState", (int)PlayerState.IDLE);
        if (pauseMenu.Paused)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = joystick.Horizontal * horizontalSensitivity;
        float z = joystick.Vertical * verticalSensitivity;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * maxSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetInteger("AnimState", (int)PlayerState.RUN);
        }
        else {
            animator.SetInteger("AnimState", (int)PlayerState.IDLE);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        Debug.Log("jumping");
        animator.SetInteger("AnimState", (int)PlayerState.JUMP);
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void SavePlayer()

    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    public void activateInvisibility()
    {
        StartCoroutine(invisibilityCoroutine());
    }

    IEnumerator invisibilityCoroutine()
    {
        Debug.Log("Invisibility Started");
        Color color = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        var mesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        invisible = true;

        mesh.material.color = new Color(1,1,1,0.1f);
        yield return new WaitForSeconds(invisibilityDuration);
        mesh.material.color = color;
        invisible = false;
        Debug.Log("Invisibility Ended");
        StopCoroutine(invisibilityCoroutine());
    }


    private void OnApplicationQuit()
    {
        // clear the inventory when the application exits
        inventory.container.Clear();
    }
}
