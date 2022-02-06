using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ending : Collidable
{
    // References
    public Image curtain;
    public TMP_Text curtainText;

    // Timers
    private float totalTimer;

    // bool
    private bool endCondition;

    // Sprite
    private SpriteRenderer spriteRenderer;
    public Sprite endSprite;

    protected override void Start()
    {
        base.Start();
        totalTimer = 0.0f;
        endCondition = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player" && GameManager2.instance.numArtifacts > 0)
        {
            endCondition = true;
            spriteRenderer.sprite = endSprite;
        }
    }
    protected override void Update()
    {
        base.Update();

        if(endCondition)
            EndCredits();
    }


    private void EndCredits()
    {
        totalTimer += Time.deltaTime;

        if(totalTimer > 3.0f){
            var tempa = curtain.color;
            tempa.a += Time.deltaTime;
            curtain.color = tempa;
        }

        if(totalTimer > 5.0f){
            var tempb = curtainText.color;
            tempb.a += Time.deltaTime;
            curtainText.color = tempb;
        }

        if(totalTimer > 10.0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Area 2");
        }

    }
}
