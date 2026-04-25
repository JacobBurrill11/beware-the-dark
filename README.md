# Beware the Dark

A 3D survival platformer built in Godot 4 using C#. Navigate a graveyard at night, avoid ghost enemies, and find the exit — all while managing how far your lantern light reaches.

## Gameplay

- Move through a 3D graveyard environment using a third-person camera
- Ghost enemies use a state machine (idle → attack) and chase the player when in range
- Stomp enemies by jumping on them; touch them while grounded and the level resets
- Fall off the level and you respawn at the last checkpoint
- Dynamic light range tied to a level progression system

## Technical Highlights

- **Enemy AI** — state machine with idle, attack, and die states; enemies path directly toward the player when within range
- **Player controller** — camera-relative movement, jump-squash animation, particle effects, and physics-based stomping
- **Light mechanic** — player's omni-light range is driven by `LevelManager`, creating difficulty scaling through visibility
- **Scene management** — separate scenes for main menu, gameplay, and end screen with a level manager singleton

## Stack

| Tool | Role |
|------|------|
| Godot 4 | Game engine |
| C# | Scripting |

## Project Structure

```
Scripts/     # C# game logic (Player, Ghost, LevelManager, UI, etc.)
Scenes/      # Godot scene files (.tscn)
Graveyard/   # 3D environment assets (.glb)
Ground/      # Terrain tile assets
Audio/       # Background music and ambient audio
Characters/  # Player character model
```

## How to Run

1. Download and install [Godot 4](https://godotengine.org/download) with .NET support
2. Clone this repo
3. Open Godot, click **Import**, and select `project.godot`
4. Press **F5** to run
