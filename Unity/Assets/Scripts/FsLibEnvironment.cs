using System.Collections;
using System.Collections.Generic;
using FsLib;
using UnityEngine;

public class InputEnvironment : Environment.IInput 
{
    public bool Up()
    {
        return Input.GetKey(KeyCode.W);
    }
    public bool Down()
    {
        return Input.GetKey(KeyCode.S);
    }
    public bool Left()
    {
        return Input.GetKey(KeyCode.A);
    }
    public bool Right()
    {
        return Input.GetKey(KeyCode.D);
    }
    public bool Attack()
    {
        return Input.GetKey(KeyCode.Space);
    }
}

public class AttackAnimationEnvironment : Environment.IAttackAnimation
{
    private readonly Animator _animator;
    public AttackAnimationEnvironment(Animator animator)
    {
        _animator = animator;
    }

    public bool IsPlaying()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("HeroAttack");
    }
}
