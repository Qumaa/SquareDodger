# Overview
A first game to ever be made by us on Unity!

The main goal of this project is to prove our capabilities to possible employers.

The project is a casual mobile game with one-hand controls and simple design. Gameplay is all about dodging obstacles by changing the character's direction with taps. The longer player remains alive, the higher score they get. After reaching certain thresholds they're allowed to choose a new game colors set of their choice.

Started May 22nd, 2023

# Summary
> **Note**
> The following information is as of 15th June, 2023

The project is build using a wide variety of principles and systems, including:
## Unity
- UI - prototype is almost finished and needs textures, shaders and animations
- Localisation - string localisation featuring three languages - English, Russian and Polish. **WIP**
- Editor scripting - custom inspectors for configuration assets. **WIP**
- Resources - loading and unloading configuration assets with Resources API. Currently only used for a fraction of assets
- Shaders - only unlit shaders for sprites and UI elements. 1/3 shaders are currently implemented
## C#
- Optimised allocations - as of current state of codebase, .Update and .FixedUpdate cycles GC allocations are both 0 bytes
- Syntax sugar - conditional access, lambda expressions, generic constraints. Planned to use LINQ, record/struct variations and async/await but there were no appropriate use cases
## Codebase
- Composition root - the entire game is launched and updated with a single MonoBehaviour script
- Architecture - game instance contains in the composition root, and its logic is split to a number of states in a state machine
- SOLID principles - although there are some necessary violations, most of the codebase fits all five principles
- Pure DI - all dependencies are injected through either constructor or properties, **except for architectural objects**
- Patterns - state machine, factory, abstract factory, fluent builder/interface, director, composite, object pooling, dependency containers
## Others
- Manual pause and reset - without using .timeScale = 0 nor reloading a scene
- Live settings update - currently only applies to themes
