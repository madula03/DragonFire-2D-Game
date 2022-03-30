using System.Collections;
using UnityEngine;


public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; //when the trap get triggered
    private bool active; //when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!triggered)            
                //trigger the firetrap
                StartCoroutine(ActivateFiretrap());
            
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite color to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //wait for the delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //turn the sprite back to notify the player
        active = true;
        anim.SetBool("activated", true);

        //wait until x seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }
}
