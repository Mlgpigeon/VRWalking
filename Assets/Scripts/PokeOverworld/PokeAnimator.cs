using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(FollowPlayer))]
public class PokeAnimator : MonoBehaviour
{
    public string pokemonName;
    private Animator _animator;
    readonly string[] NAMES =
    {
        "Movement_idle",
        "Movement_walk",
        "Movement_run"
    };
    
    // Start is called before the first frame update
    public void generateAnimator()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Templates/animTemplate") as RuntimeAnimatorController;
        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        print(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = animatorOverrideController;
        foreach (var s in NAMES)
        {
            string path = "Pokemon/"+pokemonName + "/Files/Animations/" + s;
            var animClip = Resources.Load<AnimationClip>(path);
            if (animClip != null)
            {
                animatorOverrideController[s] = animClip;
            }
        }
        
    }
    
}
