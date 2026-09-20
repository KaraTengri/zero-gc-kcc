# Zero-GC Kinematic Character Controller (KCC)

A high-performance, $O(1)$ memory allocation Kinematic Character Controller engineered for Unity. Built from the ground up to eliminate Garbage Collection overhead during high-frequency movement physics evaluations.

> **A Little Note:** I am a high school student developing this project as a core technical portfolio piece for my upcoming abroad university applications. I am aiming for true AAA-level engineering standards. Initial baseline working prototype completed right on target on **September 20, 2026**.

---

## 🎥 Phase 1: Locomotion Baseline & Profiler Proof

<!-- Videoyu GitHub web arayüzünde README düzenleme penceresine sürükleyip bıraktığında oluşan linki buraya yapıştırabilirsin -->
https://github.com/user-attachments/assets/YOUR_VIDEO_ID_HERE

> **Profiler Result:** `KCCMotor.Update` runs at strictly **0 Bytes GC Alloc** per frame under active player movement.

---

## 🛠️ Key Technical Highlights
- **Zero-GC Allocation Core**: Pure `readonly struct` state containers (`KCCStateData`, `InputData`) and static lookup tables.
- **Allocation-Free Physics**: Uses `Physics.RaycastNonAlloc` backed by global pre-allocated buffers (`NonAllocPhysicsBuffer`).
- **Input System Caching**: Integrated with Unity's New Input System using cached `InputAction` references inside `Awake` to prevent heap allocations during `ReadValue<Vector2>()`.
- **Decoupled Architecture**: Separation of orchestrator lifecycle (`KCCMotor`), mathematical calculations (`KCCMotorCore`), and surface solver logic (`SurfaceResolver`).

---

## 📑 Roadmap & Status

For detailed daily updates and technical decisions, view the full [DEVLOG.md](DEVLOG.md).

### [Phase 1 - Core Locomotion Baseline] *(Delivered Sept 20, 2026 / Active Polish)*
- [x] `KCCMotor` orchestrator with New Input System caching ($0\text{ B}$ heap allocation).
- [x] Zero-allocation ground detection using `Physics.RaycastNonAlloc`.
- [x] Zero-heap acceleration, ground friction, and spatial input projection (`KCCMotorCore`).
- [x] Verified $0\text{ B}$ GC allocation in Unity Profiler under active movement.
- [ ] **Work in Progress:** Integrating existing backend modules (such as `SurfaceProperties` structs and static lookup tables) into the primary update loop.

### [Phase 2 - Upcoming]
- [ ] Implement `CapsuleCastNonAlloc` Collision Solver (Wall Sliding & Surface Snapping).
- [ ] Slope limiting and sliding mechanics.
- [ ] Air control, gravity dynamics, and jump buffering.

---

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.  
Created by **KaraTengri**.