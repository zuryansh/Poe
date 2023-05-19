using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player Data", menuName ="Player Data")]
public class PlayerData : ScriptableObject
{
    public Player player;

    public int maxHealth;
    public int CollectibleCount;

    [Header("Run")]
    public float moveSpeed;

    [Header("Fall")]
    public float maxFallVelocity;

    [Header("Jump")]
    public int noOfJumps;
    public float jumpForce;
    public float fallGravityMultiplier;
    public float jumpCutMultiplier;

    [Header("Wall Jump")]
    public float wallSlideMultiplier;
    public float wallJumpForce;
    public float horizontalJumpForce;

    [Header("Dash")]
    public float dashForce;
    public float dashTime;
    public float dashCooldown;
    public int noOfDashes;


    public LayerMask groundLayer;
    public Vector2 groundCheckSize;

    [Header("Skills")]
    public List<PlayerSkills> skills;

    [Header("Animation")]
    public float DashAnimDuration;
    public float LandAnimDuration;
    public float HitAnimDuration;
    public float TeleportAnimDuration;

    public float invincibilityTime;

    public void SetReceiver(Player receiver) => player = receiver;

    public void AddSkill(PlayerSkills skill)
    {
        if(skills.Contains(skill)) return;
        skills.Add(skill);
    }

}
