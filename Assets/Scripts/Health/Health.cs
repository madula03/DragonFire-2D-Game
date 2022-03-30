
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
   [Header ("Health")]
   [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField]private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            //player dead
            if (!dead)
            {
                anim.SetTrigger("die");

                //player
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                //enemy
                if (GetComponent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;


                
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
                


            }
            
        }
    }
    public void Addhealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Deactivate()
    {
        EnemyCounter.enemyAmount += 1;
        gameObject.SetActive(false);
    }

}
