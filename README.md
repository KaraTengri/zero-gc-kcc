# Zero-GC Kinematic Character Controller (KCC)

A custom Kinematic Character Controller written in C# for Unity, focused entirely on zero-allocation runtime performance. Designed to run high-frequency movement math and physics queries without triggering the Garbage Collector.

### Motivation
I am building this project to create a useful, memory-clean tool for indie developers and to dive deep into low-level engine mechanics. It serves as my primary technical portfolio piece for my upcoming university applications in Computer Science. 

The baseline locomotion logic was fully validated with **0B GC Allocation** on **September 20, 2026**.

---

## 🎥 Profiler Proof & Locomotion Baseline

https://github.com/user-attachments/assets/e19ba3cc-b0ec-4cef-8db1-247f56215c20

> **Profiler Metric:** `KCCMotor.Update` runs at strictly **0 Bytes GC Alloc** per frame under continuous player movement and state changes.

---

## 🛠️ Architecture & Core Decisions

- **Allocation-Free Physics**: Replaced default Unity physics methods with `Physics.RaycastNonAlloc` backed by static `NonAllocPhysicsBuffer` arrays to avoid heap allocations during continuous ground checks.
- **Value-Type State Management**: Built `KCCStateData` and `InputData` as `readonly struct` instances to keep frame data entirely on the stack.
- **Input Caching**: Pre-cached `InputAction` references during `Awake()` to avoid heap allocations caused by `ReadValue<Vector2>()` in the execution loop.
- **Separation of Concerns**:
  - `KCCMotor`: Handles Unity event lifecycle (`Update`/`FixedUpdate`) and caches components.
  - `KCCMotorCore`: Pure static math engine for acceleration, friction, and velocity vectors.
  - `SurfaceResolver`: Evaluates normal vectors, slope limits, and surface friction values.

---

## 📑 Roadmap

For technical decisions and daily progress logs, check out [DEVLOG.md](DEVLOG.md).

### Phase 1 — Locomotion Baseline (Completed / Polishing)
- [x] Input System integration with zero runtime heap allocations.
- [x] Ground detection pipeline using `Physics.RaycastNonAlloc`.
- [x] Stack-allocated movement vector projections (`KCCMotorCore`).
- [x] Profiler validation confirming 0B GC allocation under stress tests.
- [ ] **In Progress:** Integrating lookup tables for `SurfaceProperties` to optimize dynamic material lookup.

### Phase 2 — Collision & World Interaction (Next Up)
- [ ] Sweep testing solver using `CapsuleCastNonAlloc` for wall sliding.
- [ ] Dynamic slope limit handling and slide-down velocity.
- [ ] Air dynamics, gravity curves, and configurable jump buffers.

---

## 📜 License
Distributed under the MIT License. See `LICENSE` for details.

Built with passion by **KaraTengri**.
