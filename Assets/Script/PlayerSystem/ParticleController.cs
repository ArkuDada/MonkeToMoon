using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private NewCollisionCheck newCollCheck;
    [SerializeField] private AnimationController anim;

    [Header("Particle")]
    [SerializeField] private ParticleSystem walkParticle;
    public ParticleSystem WalkParticle => walkParticle;
    [SerializeField] private ParticleSystem jumpParticle;
    public ParticleSystem JumpParticle => jumpParticle;
    [SerializeField] private float emissionRate = 30f;
    private ParticleSystem.EmissionModule emissionModule;

    public ParticleController Instance { get; private set; }

    private void Awake()
    {
        emissionModule = walkParticle.emission;
        if (playerController.IsMainCharacter) Instance = this;
    }

    void Update()
    {
        if (playerController.Horizontal > 0 | playerController.Horizontal < 0 && newCollCheck.OnGround)
        {
            emissionModule.rateOverTime = emissionRate;
        }
        else
        {
            emissionModule.rateOverTime = 0f;
            //walkingSound.Play();
        }
    }

    public void GroundTouch()
    {
        jumpParticle.Play();
    }

    public int ParticleSide()
    {
        int particleSide = newCollCheck.OnGround ? 1 : -1;
        return particleSide;
    }
}
