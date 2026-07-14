using LitMotion.Collections;
using OneM.AnimationSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LitMotion.Animation
{
    [AddComponentMenu("LitMotion Animation")]
    public sealed class LitMotionAnimation : AbstractAnimation
    {
        enum AnimationMode
        {
            Parallel,
            Sequential
        }

        [SerializeField] AnimationMode animationMode;

        [SerializeReference]
        LitMotionAnimationComponent[] components = new LitMotionAnimationComponent[0];

        Queue<LitMotionAnimationComponent> queue = new();
        FastListCore<LitMotionAnimationComponent> playingComponents;

        public IReadOnlyList<LitMotionAnimationComponent> Components => components;

        void MoveNextMotion()
        {
            if (queue.TryDequeue(out var queuedComponent))
            {
                try
                {
                    var handle = queuedComponent.Play();
                    var isActive = handle.IsActive();

                    if (isActive)
                    {
                        handle.Preserve();
                        MotionManager.GetManagedDataRef(handle, false).OnCompleteAction += MoveNextMotion;
                    }

                    queuedComponent.TrackedHandle = handle;
                    playingComponents.Add(queuedComponent);

                    if (!isActive)
                    {
                        MoveNextMotion();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }



        public bool IsActive
        {
            get
            {
                if (queue.Count > 0) return true;

                foreach (var component in playingComponents.AsSpan())
                {
                    var handle = component.TrackedHandle;
                    if (handle.IsActive()) return true;
                }

                return false;
            }
        }


    }
}