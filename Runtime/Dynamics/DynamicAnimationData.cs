using UnityEngine;

namespace OneM.AnimationSystem
{
    /// <summary>
    /// Animation container for dynamic animations, which can be played by the <see cref="DynamicAnimator"/> component.
    /// </summary>
    /// <remarks>
    /// Holds an optional Start clip (played once), a Loop clip (played while playing)
    /// and an optional End clip (played once when stopped).<br/>
    /// Create one asset per action (e.g. sitting, sweeping) and play it on any character with a
    /// <see cref="DynamicAnimator"/>, without creating new Animator Controllers.
    /// </remarks>
    [CreateAssetMenu(fileName = "NewDynamicAnimationData", menuName = "OneM/Animation System/Dynamic Animation Data")]
    public sealed class DynamicAnimationData : ScriptableObject
    {
        /// <summary>
        /// Optional. The animation played once, before the <see cref="Loop"/> animation.
        /// </summary>
        [field: SerializeField, Tooltip("[Optional] Animation to play when Start.")]
        public AnimationClip Start { get; private set; }

        /// <summary>
        /// The animation played in a loop, while the Dynamic Animator is playing.
        /// </summary>
        [field: SerializeField, Tooltip("Animation to play in a loop.")]
        public AnimationClip Loop { get; private set; }

        /// <summary>
        /// Optional. The animation played once, after the Dynamic Animator stops playing.
        /// </summary>
        [field: SerializeField, Tooltip("[Optional] Animation to play when End.")]
        public AnimationClip End { get; private set; }
    }
}