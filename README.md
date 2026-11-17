# Animation System

* Scripts to improve animation workflow
* Unity minimum version: **6000.3**

## Summary

Components to animate GameOjects at runtime.

In the Inspector, click on the **Add Component** button > **Animation** and select the category to start to animate.

## How to Use

### Dynamic Animator

Plays **Start**, **Loop** and **End** animations set at runtime, without creating new Animator Controllers.

- `DynamicAnimationData` (**Create > OneM > Animation System > Dynamic Animation Data**): holds the optional Start, the Loop and the optional End clips.
- `DynamicAnimator`: put it next to the Animator. It overrides the `Human@Dynamic_Start`, `Human@Dynamic_Loop` and `Human@Dynamic_End` clips of the Animator Controller **Dynamic** Sub-State Machine.
  - `LoadAndPlay(data)`: loads the clips and plays Start (if any), then Loop.
  - `Stop()`: plays End (if any). `ClearAnimations()`: stops and removes all clips.
  - Animations can only be loaded after its `Start`.
- `DynamicAnimatorPlayer`: loads and plays a `DynamicAnimationData` when the Scene starts (runs after the Dynamic Animator Start).

## Installation

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set and the correct git credentials to 1M Bits Horde.

- In this repo, go to Code button, select SSH and copy the URL.
- In Unity, use the **Package Manager** "Add package from git URL..." feature and paste the URL.
- Set the version adding the suffix `#[x.y.z]` at URL

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>
