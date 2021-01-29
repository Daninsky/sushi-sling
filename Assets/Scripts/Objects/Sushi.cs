using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(ParticleSystem), typeof(Rigidbody2D))]
public class Sushi : MonoBehaviour
{
    public string Name;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private ParticleSystem particles;

    public bool hasHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        particles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        sprite.enabled = true;
    }

    private void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            StartCoroutine(SelfDestruct());
        }
    }


    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator Hit()
    {
        hasHit = true;
        StopCoroutine(SelfDestruct());
        particles.Play();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
        hasHit = false;
    }


}
