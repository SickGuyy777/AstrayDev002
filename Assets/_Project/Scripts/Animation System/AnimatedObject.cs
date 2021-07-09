using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// An object with animation
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedObject : MonoBehaviour
{
    [Tooltip("The animations this object holds")]
    [SerializeField]
    private List<Animation> animations = new List<Animation>();

    [field: Tooltip("The current index of the playing animation (-1 = not playing)")]
    [field: Readonly]
    public int Playing { get; private set; } = -1;

    [field: Tooltip("The current index of the playing sprite (-1 = full animation)")]
    [field: Readonly]
    public int Sprite { get; private set; } = -1;
    private SpriteRenderer _renderer;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// Referencing <see cref="_renderer"/>.
    /// </summary>
    private void Awake() => _renderer = GetComponent<SpriteRenderer>();
    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// Starting <see cref="AnimationUpdate"/>.
    /// </summary>
    private void Start() => StartCoroutine(AnimationUpdate());

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// Updating animation indexes
    /// </summary>
    private void OnValidate()
    {
        for (int i = 0; i < animations.Count; i++)
            animations[i].animationIndex = i;
    }

    /// <summary>
    /// Play <paramref name="index"/>.
    /// </summary>
    /// <param name="index"></param>
    public void PlayAnimation(int index, bool force)
    {
        if (index < 0 || index >= animations.Count || index == Playing)
            return;

        if (force)
            StopAnimation();

        Sprite = -1;
        Playing = index;
        _renderer.sprite = animations[Playing].sprites[0];
    }

    /// <summary>
    /// Play <paramref name="name"/>.
    /// </summary>
    /// <param name="index"></param>
    public void PlayAnimation(string name, bool force)
    {
        int index = GetAnimationIndexFromName(name);

        if (index == Playing)
            return;

        if (force)
            StopAnimation();

        Sprite = -1;
        Playing = index;
        _renderer.sprite = animations[Playing].sprites[0];
    }

    /// <summary>
    /// Get an <see cref="Animation"/> with the name <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private int GetAnimationIndexFromName(string name)
    {
        int index = -1;

        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i].animationName == name)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogException(new System.MissingMemberException($"No animation with name '{name}' found."), gameObject);

        return index;
    }

    /// <summary>
    /// Stop playing animations.
    /// </summary>
    public void StopAnimation()
    {
        Playing = -1;
        StopCoroutine(PlayAnimation(Playing));
    }

    /// <summary>
    /// Play a specific sprite.
    /// </summary>
    /// <param name="animationIndex"></param>
    /// <param name="spriteIndex"></param>
    public void PlaySprite(int animationIndex, int spriteIndex)
    {
        Assert.IsFalse(animationIndex >= animations.Count, "Animation index is out of range.");
        Assert.IsFalse(spriteIndex >= animations[animationIndex].sprites.Length, "Sprite index is out of range.");

        if (animationIndex == Playing)
            return;

        Playing = animationIndex;
        Sprite = spriteIndex;
    }

    /// <summary>
    /// The animation update cycle
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimationUpdate()
    {
        while (true)
        {
            yield return new WaitUntil(() => Playing >= 0);
            while (Playing >= 0)
            {
                if (nRunning > 0)
                    break;

                if (Playing >= 0)
                    StartCoroutine(PlayAnimation(Playing));

                yield return new WaitUntil(() => nRunning == 0);
            }
        }
    }

    int nRunning = 0;
    /// <summary>
    /// Play <see cref="Playing"/>.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAnimation(int currentAnim)
    {
        nRunning++;
        foreach (var sprite in animations[currentAnim].sprites)
        {
            if (Sprite > -1)
            {
                if (Sprite < 0 || Playing != currentAnim)
                    break;

                _renderer.sprite = animations[currentAnim].sprites[Sprite];
                break;
            }

            _renderer.sprite = sprite;

            yield return new WaitForSeconds(animations[currentAnim].cooldown);

            if (Playing != currentAnim || nRunning > 1)
                break;
        }

        nRunning--;
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

    [Space]

    [Readonly]
    public int animationIndex;
}
