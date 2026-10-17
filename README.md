# Unity Dialogue System

A branching dialogue system built for Unity using xNode. Developers can create their own dialogue graphs by connecting nodes to display dialogye, give the player choices and control the scene.

This was originally developed for The Story of Kate Webster, this project is seperated from that game into its own standalone Unity project with a demo scene.

## Features
- Visual dialogue graph editing using xNode.
- Dialogue text and speaker names.
- Branching choices with separate end connections.
- Audio playback.
- Animation support.
- Camera switching.
- Scene object actions.
- Timed waits.
- PlayerPrefs saving.
- Extendable node classes for custom behaviour.

## Requirements

The current project uses Unity `6000.3.19f1`

**Dependendies**
- xNode
- Unity Input System
- TextMeshPro

The required packages are saved in `Packages/manifest.json`. Unity will restore them when opening the project for the first time. Git must be installed for Unity to install the xNode Git dependency.

## Installation

**Import the Unity Package**
1. Create or open a project using Unity `6000.3.19f1`.
2. Install the dependencies listed above before importing the package.
3. In Unity, go to `Assets > Import Package > Custom Package`.
4. Select `UnityDialogueSystem-V2.unitypackage`.
5. Click `Import`
6. Check the console for errors.

**Clone the full project**
Alternatively, you can clone the repository to open the complete Unity project

`git clone https://github.com/AlekMunroe/Unity-Dialogue-System.git`

1. Open Unity Hub
2. Add the cloned folder into the Unity Hub.
3. Open it using Unity `6000.3.19f1`
4. Allow Unity to import the assets and restore all the packages.
5. Check the console for errors.

## Running the Demo
1. Open `Assets/DialogueSystem/Demo/Scenes/DialogueDemo.unity`
2. Check the `DialogueTrigger` component has:
	- A dialogue graph
	- The scene's dialogue manager
	- **Trigger Options** set to **Start On Awake**
4. Enter Play Mode.

The scene is configured to use `Assets/DialogueSystem/DialogueGraphs/DemoDialogueGraph.asset`
Double click on the `DemoDialogueGraph` to inspect its nodes and connections.

## How the System Works
The system has three parts:
- DialogueGraph: This holds the connected dialogue nodes.
- DialogueTrigger: This starts a graph and enables the **dialogue manager**. It can start when a trigger is enabled, after a delay or when the `Player` tag enters a trigger collider.
- DialogueManager: This handles the current node and updates the interface.

A simple dialogue follows the structure `Start > Monologue > End`

A branching conversation uses a **Choice Node** where each output connects to a different route.

Some nodes have waiting for player input. Others can perform actions and continue automatically.

## Project Structure
```
Assets/
├── Demo/
│   └── Scenes/
│       └── DialogueDemo.unity
└── DialogueSystem/
    ├── DialogueGraphs/
    ├── Nodes/
    ├── Triggers/
    ├── DialogueGraph.cs
    ├── DialogueManager.cs
    ├── DialogueTrigger.cs
    └── DialogueUtilities.cs

Documentation/
└── DeveloperGuide.md
```

## Creating and Extending Dialogue
See the [Developer guide](Documentation/DeveloperGuide.md) to learn how to:
- Create and trigger a dialogue graph.
- Adding choices and connecting branches.
- Configuring existing node types.
- Creating custom nodes with examples.
- Connecting custom node behaviour to the dialogue manager.

Custom nodes inherit directly from `DialogueNode`. The graph editor hides fields inherited from the DialogueNode by default. Nodes that may require those fields can override `ShowDialogueFields` to return true.

## Troubleshooting
### The dialogue doesn't start
Check the trigger GameObject is enabled and its graph and dialogue manager references are correctly assigned. Also make sure the trigger option is correct for how you expect to start the conversation.

The graph also requires a `StartNode` connected in the dialogue graph.

### Dialogue buttons don't respond
Check the scene has an `EventSystem` and it is setup to use the Input System UI Input Module.

Check that **Active Input Handling** is set to **Input System Package (New)**.

### A "not found" error appears in the console
Check the nodes are configured and the names match in the scene correctly.

## Documentation

- [Developer guide](Documentation/DeveloperGuide.md)

## Author
Alek Paul Munroe

[GitHub](https://github.com/AlekMunroe/)