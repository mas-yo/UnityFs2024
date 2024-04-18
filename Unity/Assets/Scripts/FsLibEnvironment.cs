using System.Collections;
using System.Collections.Generic;
using FsLib;
using UnityEngine;

public class InputEnvironment : Environment.IInput 
{
    public bool Up()
    {
        return UnityEngine.Input.GetKey(KeyCode.W);
    }
    public bool Down()
    {
        return UnityEngine.Input.GetKey(KeyCode.S);
    }
    public bool Left()
    {
        return UnityEngine.Input.GetKey(KeyCode.A);
    }
    public bool Right()
    {
        return UnityEngine.Input.GetKey(KeyCode.D);
    }
    public bool Attack()
    {
        return UnityEngine.Input.GetKey(KeyCode.Space);
    }
}

public class AttackAnimationEnvironment : Environment.IAttackAnimation
{
    private readonly Animator _animator;
    public AttackAnimationEnvironment(Animator animator)
    {
        _animator = animator;
    }
    public void Play()
    {
        _animator.Play("HeroAttack");
    }

    public bool IsPlaying()
    {
        return false;
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("HeroAttack");
    }
}
