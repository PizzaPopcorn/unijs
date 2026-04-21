# Changelog

## [1.0.0] - 2026-04-20

### First Release

*Fundamental bridge functionality.*

- Bi-directional Event-Driven Communication (`JSEventHub`, `Unity.InvokeEvent`)
- Basic `GameObject` & `Transform` Proxying (Position, Rotation, Scale, Active State)
- Basic Physics Support (`Rigidbody` AddForce/AddTorque, velocity)
- Resource Loading & Instantiation (`Resources.Load`, `Instantiate`)
- [Basic Scene Management (`LoadScene`, Loading Progress)
- Lifecycle Synchronization: JS proxy objects are automatically invalidated or updated when the corresponding Unity object is destroyed.