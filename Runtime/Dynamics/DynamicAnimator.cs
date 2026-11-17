using UnityEngine;

namespace OneM.AnimationSystem
{
    /// <summary>
    /// Dynamic Controller that allows to set Start, Loop and End animations at runtime.
    /// </summary>
    /// <remarks>
    /// At Start, it replaces the Animator Controller with an <see cref="AnimatorOverrideController"/>.
    /// Loading a <see cref="DynamicAnimationData"/> overrides the <c>Human@Dynamic_Start</c>, <c>Human@Dynamic_Loop</c>
    /// and <c>Human@Dynamic_End</c> clips used by the Animator Controller <b>Dynamic</b> Sub-State Machine.<br/>
    /// The Animator Controller must have this Sub-State Machine and the <c>IsDynamicPlaying</c>,
    /// <c>HasDynamicStart</c>, <c>HasDynamicLoop</c> and <c>HasDynamicEnd</c> bool parameters.<br/>
    /// Animations can only be loaded after this component Start. Used by Npcs.
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class DynamicAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        #region PARAMS
        /// <summary>
        /// Whether the dynamic animations are playing (Animator "IsDynamicPlaying" parameter).
        /// </summary>
        public bool IsDynamicPlaying
        {
            get => animator.GetBool(isDynamicPlaying);
            private set => animator.SetBool(isDynamicPlaying, value);
        }

        /// <summary>
        /// Whether a Start animation is loaded (Animator "HasDynamicStart" parameter).
        /// </summary>
        public bool HasDynamicStart
        {
            get => animator.GetBool(hasDynamicStart);
            private set => animator.SetBool(hasDynamicStart, value);
        }

        /// <summary>
        /// Whether a Loop animation is loaded (Animator "HasDynamicLoop" parameter).
        /// </summary>
        public bool HasDynamicLoop
        {
            get => animator.GetBool(hasDynamicLoop);
            private set => animator.SetBool(hasDynamicLoop, value);
        }

        /// <summary>
        /// Whether an End animation is loaded (Animator "HasDynamicEnd" parameter).
        /// </summary>
        public bool HasDynamicEnd
        {
            get => animator.GetBool(hasDynamicEnd);
            private set => animator.SetBool(hasDynamicEnd, value);
        }

        private static readonly int isDynamicPlaying = Animator.StringToHash("IsDynamicPlaying");
        private static readonly int hasDynamicStart = Animator.StringToHash("HasDynamicStart");
        private static readonly int hasDynamicLoop = Animator.StringToHash("HasDynamicLoop");
        private static readonly int hasDynamicEnd = Animator.StringToHash("HasDynamicEnd");
        #endregion

        private AnimatorOverrideController overrideController;

        private void Reset() => animator = GetComponent<Animator>();
        private void Start() => SetupOverrideController();

        /// <summary>
        /// Whether the Animator is in any state of the Dynamic Sub-State Machine (Start, Loop or End).
        /// </summary>
        public bool IsRunningDynamicSubState()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName("Base Layer.Dynamic.Start")
                || stateInfo.IsName("Base Layer.Dynamic.Loop")
                || stateInfo.IsName("Base Layer.Dynamic.End");
        }

        /// <summary>
        /// Plays the loaded animations: Start (if any), then Loop.
        /// </summary>
        public void Play() => IsDynamicPlaying = true;

        /// <summary>
        /// Stops the loaded animations, playing the End animation (if any).
        /// </summary>
        public void Stop() => IsDynamicPlaying = false;

        /// <summary>
        /// Loads the given animations and plays them.
        /// </summary>
        /// <param name="data">The animations to load.</param>
        public void LoadAndPlay(DynamicAnimationData data)
        {
            LoadAnimations(data);
            Play();
        }

        /// <summary>
        /// Loads the given Start, Loop and End animations, without playing them.
        /// </summary>
        /// <param name="data">The animations to load.</param>
        public void LoadAnimations(DynamicAnimationData data)
        {
            SetStartAnimation(data.Start);
            SetLoopAnimation(data.Loop);
            SetEndAnimation(data.End);
        }

        /// <summary>
        /// Stops and removes all loaded animations.
        /// </summary>
        public void ClearAnimations()
        {
            Stop();
            SetStartAnimation(null);
            SetLoopAnimation(null);
            SetEndAnimation(null);
        }

        private void SetStartAnimation(AnimationClip clip)
        {
            overrideController["Human@Dynamic_Start"] = clip;
            HasDynamicStart = clip != null;
        }

        private void SetLoopAnimation(AnimationClip clip)
        {
            overrideController["Human@Dynamic_Loop"] = clip;
            HasDynamicLoop = clip != null;
        }

        private void SetEndAnimation(AnimationClip clip)
        {
            overrideController["Human@Dynamic_End"] = clip;
            HasDynamicEnd = clip != null;
        }

        private void SetupOverrideController()
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = overrideController;
        }
    }
}