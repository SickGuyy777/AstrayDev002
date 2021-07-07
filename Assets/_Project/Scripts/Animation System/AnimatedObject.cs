using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedObject : MonoBehaviour
{
    [Tooltip("The animations this object holds")]
    [SerializeField]
    private List<Animation> animations = new List<Animation>();

    private int playing = -1;
    private SpriteRenderer _renderer;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Awake() => _renderer = GetComponent<SpriteRenderer>();
    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    private void Start() => StartCoroutine(AnimationUpdate());

    /// <summary>
    /// Play <paramref name="index"/>.
    /// </summary>
    /// <param name="index"></param>
    public void PlayAnimation(int index)
    {
        if (index < 0 || index >= animations.Count)
            return;

        playing = index;
    }
    /// <summary>
    /// Stop playing animations.
    /// </summary>
    public void StopAnimation() => playing = -1;

    /// <summary>
    /// The animation update cycle
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimationUpdate()
    {
        while (true)
        {
            yield return new WaitUntil(() => playing >= 0);
            while (playing >= 0)
            {
                StartCoroutine(PlayAnimation());
                yield return new WaitForSeconds(animations[playing].cooldown * animations[playing].sprites.Length);
            }
        }
    }

    /// <summary>
    /// Play <see cref="playing"/>.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAnimation()
    {
        foreach (var sprite in animations[playing].sprites)
        {
            _renderer.sprite = sprite;

            if (playing < 0)
                yield break;

            yield return new WaitForSeconds(animations[playing].cooldown);
        }
    }
}

/// <summary>
/// An animation - has a name, a cooldown and a <see cref="Sprite"/>[].
/// </summary>
[System.Serializable]
public class Animation
{
    public string animationName = "Animation";
    public float cooldown = 0.02f;
    public Sprite[] sprites;
}
