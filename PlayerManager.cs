using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerAnimations playerAnimations;
    [HideInInspector]
    public PlayerCombat playerCombat;
    [HideInInspector]
    public PlayerStats playerStats;
    [HideInInspector]
    public PlayerMovement playerMovement;

    [Header("Ground Check")]
    [SerializeField] float grounddist;
    [SerializeField] LayerMask playerMask;

    [HideInInspector]
    public GameObject spawnLocation;
    [HideInInspector]
    public Rigidbody2D _rb;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip PlayerAttackClip;
    public AudioClip playerDiesClip;
    public GameObject canvas;

    [Header("Flags")]
    public bool isGrounded = true;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        _rb = GetComponent<Rigidbody2D>();

    }

    private void LateUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, grounddist, playerMask);
        Debug.DrawRay(transform.position, Vector2.down * grounddist, Color.red);
        // Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
