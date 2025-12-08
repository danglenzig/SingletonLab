using UnityEngine;
using Constants;


public enum EnumPlayerAnimations
{
    IDLE,
    WALK
}

public class PlayerSprite : MonoBehaviour
{

    public event System.Action<EnumPlayerAnimations> OnAnimationFinishedEvent;
    public event System.Action<EnumPlayerAnimations> OnAnimationLoopedEvent;

    //[SerializeField] private GameObject playerSprite;

    private SpriteRenderer myRenderer;
    private Animator myAnimator;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        myAnimator.Play(PlayerAnimations.IDLE);
    }


    public void PlayAnimation(EnumPlayerAnimations anim)
    {
        switch (anim)
        {
            case EnumPlayerAnimations.IDLE:
                myAnimator.Play(PlayerAnimations.IDLE);
                return;
            case EnumPlayerAnimations.WALK:
                myAnimator.Play(PlayerAnimations.WALK);
                return;
        }
    }

    public void SetFlipX(bool _flipX)
    {
        //Debug.Log(_flipX);
        myRenderer.flipX = _flipX;
    }
    public void ToggleFLipX()
    {
        myRenderer.flipX = !myRenderer.flipX;
    }

    public bool GetFlipX()
    {
        return myRenderer.flipX;
    }

    private void OnAnimationLooped(EnumPlayerAnimations animName)
    {
        OnAnimationLoopedEvent?.Invoke(animName);
    }

    private void OnAnimationFinished(EnumPlayerAnimations animName)
    {
        OnAnimationFinishedEvent?.Invoke(animName);
    }


}
