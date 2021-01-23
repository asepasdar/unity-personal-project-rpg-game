using RPG.Pooler.Base;
using System.Collections;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI TextMesh;

    float lengthDuration = 0f;

    void Start()
    {
        AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo(0);
        lengthDuration = clipinfo[0].clip.length;
    }

    public void SetText(string text) {
        animator.Play("DamageSlide");
        TextMesh.SetText(text);
        StartCoroutine(BackToPool());
    }

    IEnumerator BackToPool() {
        yield return new WaitForSecondsRealtime(lengthDuration + .5f);
        PoolerData.instance.BackToPool("Damage", gameObject);
    }
}
