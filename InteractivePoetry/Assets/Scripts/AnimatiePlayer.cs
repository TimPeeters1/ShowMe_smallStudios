using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiePlayer : MonoBehaviour
{
    Animator anim;
    public string currentClip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().enabled = false;
        anim.StopPlayback();
    }

    public IEnumerator playAnim(AnimationClip clip)
    {
        GetComponent<SpriteRenderer>().enabled = true;

        anim.Play(currentClip, -1, 0f);
        yield return new WaitForSeconds(0.8f);
        anim.StopPlayback();
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
