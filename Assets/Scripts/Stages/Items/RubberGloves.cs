using Assets.Scripts.Stages;
using System.Collections;
using UnityEngine;

public class RubberGloves : Item
{
    [SerializeField] private Animator _animator;
    


    public override void ActionInPreview()
    {

        _animator.SetTrigger("CheckStart");
        AddAction("-проверить на прокол(заменить в случае обнаружения)");
    }


    public void EndAnimation()
    {
        EndedAnimation();
    }

}