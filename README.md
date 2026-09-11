# Zero-GC Kinematic Character Controller (KCC)

A high-performance, $O(1)$ memory allocation Kinematic Character Controller designed for Unity. Built from the ground up to eliminate Garbage Collection overhead during high-frequency movement physics evaluations.

---

## 🛠️ Key Technical Highlights
- **Zero-GC Allocation Core**: Pure `readonly struct` state containers and static lookup tables.
- **Hybrid Data Model**: Lightweight struct-driven evaluations decoupled from heavy `MonoBehaviour` inspector bloat.
- **Advanced Dynamics**: Custom slope handling, Coyote Time, Jump Buffering, and custom collision solvers.

---

## 📑 DevLog & Progress Tracker

### [Day 0 - Preparation & Infrastructure]
- Initialized GitHub repository structure with Git LFS and strict Unity `.gitignore`.
- Configured MIT Licensing.
- Finalized architecture plan: `SurfaceProperties` struct & `KCCMotorCore` static lookup table design.

### [Phase 1 - In Progress]
- [ ] Implement `SurfaceProperties` immutable struct.
- [ ] Construct static surface lookup table.
- [ ] Establish initial unit tests & benchmark markers.

---

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
Created by **KaraTengri**.