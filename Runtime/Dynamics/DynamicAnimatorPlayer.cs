using UnityEngine;

namespace OneM.AnimationSystem
{
    /// <summary>
    /// Loads and plays a <see cref="DynamicAnimationData"/> in a <see cref="DynamicAnimator"/> when the Scene starts.
    /// </summary>
    /// <remarks>
    /// Runs after the <see cref="DynamicAnimator"/> Start, since the animations can only be loaded after it.
    /// </remarks>
    [DefaultExecutionOrder(1)]
    [DisallowMultipleComponent]
    public sealed class DynamicAnimatorPlayer : MonoBehaviour
    {
        [SerializeField] private DynamicAnimator animator;
        [SerializeField] private DynamicAnimationData data;

        private void Start() => TryPlayData();

        private void TryPlayData()
        {
            if (data) animator.LoadAndPlay(data);
        }
    }
}