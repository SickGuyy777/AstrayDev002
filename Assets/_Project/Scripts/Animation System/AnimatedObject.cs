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

    private bool inLoop;

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
    /// Stop playing animations.
    /// </summary>
    public void StopAnimation()
    {
        Playing = -1;
        StopCoroutine(PlayAnimation(Playing));
        inLoop = false;
    }

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
        inLoop = true;
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
        inLoop = false;
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
