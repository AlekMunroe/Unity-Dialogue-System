# Unity Dialogue System Developer Guide

## Purpose

This guide explains how to create dialogue graphs and existing nodes along with configuring. It also explains how to extend a system with custom nodes.

For insallation instructions see the [README](../README.md).

## System overview

| Script | Responsibility |
| - | - |
| DialogueGraph.cs | Holds nodes and finds the start node |
| DialogueNode.cs | Handles the shared input, dialogue fields and the continuation |
| DialogueManager.cs | Processes the nodes and updates the interface along with handling the choices |
| DialogueTrigger.cs | Starts a graph using the selected trigger option |
| DialogueUtilities.cs | Looks for GameObjects by their names |
| Editor/DialogueNodeEditor.cs | Controls the visible fields inside the graph window |
| Triggers/ObjectBasedDialogueContinuation.cs | Requests dialogue continuation when a trigger is entered |

These paths are all under `Assets/DialogueSystem/` as the root.

## Dialogue Flow

A simple dialogue follows the structure `Start > Monologue > End`

A monologue node shows its content and prepares the next node. When clicking next, it then continues onto the next node.

Action nodes perform their functions and continue automatically.

## The Demo
Open: `Assets/DialogueSystem/Demo/Scenes/DialogueDemo.unity`

Before entering Play Mode:
1. Check the `DialogueTrigger-EnableThisToStart` GameObject is enabled.
2. Check the `DialogueTrigger` component has:
	- A dialogue graph
	- The scene's dialogue manager
	- **Trigger Options** set to **Start On Awake**
3. Check the **Trigger Options** is set to **Start On Awake**.

When entering Play Mode, the dialogue system will start automatically. Use the UI buttons to explore the paths.

To inspect the graph that is used, select the trigger's **DialogueGraph** asset and open it in xNode's Graph Editor.

## Create a Dialogue Graph
### Create the Graph Asset
1. In the project window go to the folder where you want to save your graph
2. Right click, select `Create > Dialogue > DialogueGraph` and name it.
3. Double click the newly created DialogueGraph to open the graph editor.

## Add Nodes
Right click in an empty area of the graph and add:
- StartNode
- MonologueNode
- EndNode

Connect the nodes: 
```
Start.nextNode > Monologue.input
Monologue.nextNode > End.input
```

Each graph should have only one start node.

### Input dialogue
On the **Monologue Node** input:

| Field | Example |
| - | - |
| Speaker Name | Bob |
| Dialogue Text | Hey! How are you? |

AUdio and animations can be empty for a text only conversation.

### Run the Graph
The quickest way to test the graph is to use a copy of the demo scene:
1. Save the demo scene under a new name.
2. Select the **DialogueTrigger**.
3. Add your new graph to **Dialogue Graph**.
4. Keep the existing **Dialogue Manager**.
5. Set **Trigger Options** to **Start On Awake**.
6. Enter Play Mode.

The monologue you created should show. Clicking next should complete the dialogue using the end node.

## Branching choices
In the graph create this structure:

```
Start > Monologue > Choice
		      ├─ Choice 0 → Monologue A → End
		      └─ Choice 1 → Monologue B → End
```

### Configure the Choice Node
1. Add a **ChoiceNode**.
2. Enter the **Speaker Name** and **Dialogue Text**.
3. Set the choices array to 2.
4. Enter text for both choices.

Example:

| Choice | Text |
| - | - |
| 0 | I am good, how are you? |
| 1 | Goodbye. |

The node creates outputs for both choices.

Connect each output to the input of the node you want to continue with in each choice. Both responses can connect to an end node.

### Choices outcome
When a choice node is processed:
- The manager should the choice of text and buttons
- The next button is hidden
- Selecting a button continues the graph with the assigned output
- The selected route continues immediately.

Setup the choices before connecting the output. After changing the node, check that every input and output still lead to the correct places.

## Connecting the Dialogue to a Scene
The DialogueManager needs references to the scenes interface.

| Field | Assign |
| - | - |
| Speaker Text | TextMeshPro text component displaying the speakers name |
| Dialogue Text | TextMeshPro text component displaying the dialogue text |
| Dialogue Object | Root object of the displayed dialogue content |
| Choice Button Prefab | Button prefab used for each choice |
| Choice Button Container | Transform under which choice buttons are created |
| Monologue UI | The UI object controlled by HideMonologueNode |
| Next Button | GameObject with the Next button |

The choice button prefab must include a Unity UI button component on it's root and a TextMeshPro text benieth.

On the Next button's **OnClick()** event:
1. Add the scene's **DialogueManager**.
2. Select `DialogueManager > OnNextButtonClicked()`

## UI Input

The dialogue scripts use UI button clicks instead of reading keyboard or mouse input directly.

The demo uses Unity's Input System using the EventSystem's **Input System UI Input Module**.

When creating your own scene:

1. Check the scene has an **EventSystem**.
2. Check it uses an **Input System UI Input Module**.
3. Check the Canvas has a **Graphic Raycaster**.
4. Check the Next button calls `DialogueManager > OnNextButtonClicked()`.

## Dialogue Trigger

Add a **DialogueTrigger** component to a GameObject in the scene.

Assign:

- **Dialogue Graph**: The graph you want to run.
- **Dialogue Manager**: The manager in the scene.
- **Trigger Options**: How the dialogue starts.

| Option | What it does |
| - | - |
| Start On Awake | Starts the dialogue when the component becomes active and its Start method runs |
| Start On Trigger | Starts when an object tagged Player enters the trigger |
| Start After Time | Starts after the number of seconds entered in Start Delay Time |

The **Start On Awake** option uses the script's `Start()` method.

If **Destroy On Start** is enabled, the GameObject with the trigger is destroyed after the dialogue starts. Keep the trigger on a separate GameObject if you want other object not being destroyed.

### Start On Trigger

To start the dialogue when the player enters an area:

1. Add a 3D Collider to the trigger GameObject.
2. Enable **Is Trigger**.
3. Set the entering object's tag to **Player**.
4. Make sure at least one of the objects has a Rigidbody.
5. Check the physics layers allows the objects to interact.
6. Assign the dialogue graph and manager.
7. Set **Trigger Options** to **Start On Trigger**.

## Existing Nodes

### StartNode

Marks the start of the graphs.

Connect the output to the first node you want to show. Each graph should have only one StartNode.

### MonologueNode

Displays the speaker's name and dialogue text.

It also supports:

- Audio clips.
- Animation clips.

Audio and animation fields can be empty for a text only conversation.

The **Object Name** field names the temporary GameObject created for audio playback. It does not need to match an existing scene object.

Clicking Next continues to the connected node.

### ChoiceNode

Displays dialogue and creates a button for each choice.

Set the choices array size and enter the text for each button. Connect each choice output to the node you want that choice to continue with.

The node also supports the same dialogue, audio and animation fields as MonologueNode.

### EndNode

Ends the conversation by clearing the text and stopping the DialogueManager GameObject.

### WaitNode

Waits for the number of seconds entered in **Delay Time** it then continues automatically.

The Next button is hidden during the wait. The dialogue text stays unless another node clears it.

### PlayerPrefNode

Saves a String, Int or Float value using Unity's PlayerPrefs.

Set:

- **Player Pref Name**: The key used to save the value.
- **Pref Type**: The type of value.
- The value field matching the selected type.

Example:

| Field | Value |
| - | - |
| Player Pref Name | MetBob |
| Pref Type | Int |
| Int Value | 1 |

Only the value matching the selected type is saved.

The saved value stays between runs. Other scripts reading it should use the same key and type.

This node saves the values. It does not read them or change the dialogue route.

### CameraSwitchNode

Disables one camera and enables one camera.

| Field | What it does |
| - | - |
| Disable Cam | Name of the camera GameObject to disable |
| Enable Cam | Name of the camera GameObject to enable |
| Player Cam | (optional) The player's camera GameObject name |
| Disable Player Cam | Controls if the player camera is disabled or enabled |

Leave **Player Cam** empty if you do not need to change it.

If a Player Cam name is entered:

- Enabling **Disable Player Cam** disables that GameObject.
- Disabling **Disable Player Cam** enables that GameObject.

**Disable Cam** and **Enable Cam** still need GameObject names.

### EnableObjectNode

Enables or disables a GameObject.

Enter the object's name in **Object Name** and set **Enable Object** if you want it enabled.

### DestroyNode

Destroys the GameObject entered in **Object To Destroy**, then continues to the next node.

### MoveObjectNode

Moves the named GameObject to the position entered.

The position is local to the object's parent, not global. The object will move immediately.

### PlayAudioNode

Plays the **Sfx Audio Clip** and continues without waiting for the clip to finish.

**Object Name** names the temporary GameObject to play the audio.

### EnableMouseNode

Controls the cursor's visibility and lock state.

The DialogueManager also makes the cursor visible and unlocked when the dialogue object is active.

### HideMonologueNode

Enables or disables the GameObject assigned to the **Monologue UI**.

Set **Enable UI**. The graph will then continues automatically.

### ClearNode

Clears the speaker and dialogue text, stops the current dialogue audio and hides the dialogue object.

The next node does not continue automatically. Use the continuation event when you want the dialogue to continue.

### SwitchSceneNode

Loads the scene from the **Scene Index** after 1.5 seconds.

Add the scene to the active build profile's scene list and check the index.

### CommentNode

Adds developer notes to the graph.

It does not have connections and isnt used for dialogue. Use it similar to comments.

## Audio and Animation

### Dialogue Audio

MonologueNode and ChoiceNode can play audio clips.

The manager creates a temporary AudioSource at the camera's position. The scene needs an enabled camera and AudioListener.

### Animation
To use animation:

1. Add an **Animation Clip**.
2. Enter the GameObject's name in **Target Animation Object Name**.

The system does not set the animatior or play animator controller states.

## Continue Dialogue Using Scene Triggers

**ObjectBasedDialogueContinuation** is used in the same wa yas clicking Next.

This can be used after a ClearNode when the dialogue should continue after a scene interaction.

To set it up:

1. Add **ObjectBasedDialogueContinuation** to the object that will enter the trigger.
2. Configure the Rigidbody and Collider.
3. Create the tag for **DialogueContinuationTrigger**.
4. Set thge tag to the target trigger GameObject.
5. Input the target trigger's exact name in **Trigger Name**.
6. Enable **Is Trigger** on the target's Collider.

When the name and tag match, the script will call:

```csharp
DialogueManager.instance.OnNextButtonClicked();
```

If **Destroy On Trigger** is enabled, it destroys the GameObject with ObjectBasedDialogueContinuation.

This does not select a choice. Next request is ignored while the manager is waiting for the player to choose.

## Creating a Custom Node

Creating a custom node has two parts:

1. Create the node script.
2. Add its functionality to DialogueManager.

It inherits from DialogueNode but does not make the manager start the next node.

This is an example of a node that writes a message to the console:
### Create the Node Script

Create `LogMessageNode.cs` in:

`Assets/DialogueSystem/Nodes/`

Add:

```csharp
using UnityEngine;

[CreateAssetMenu(
    fileName = "LogMessageNode",
    menuName = "Dialogue/LogMessageNode")]
public class LogMessageNode : DialogueNode
{
    [TextArea]
    public string message;

    [Output]
    public DialogueNode nextNode;

    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}
```

This node:

- Gets the input connection from DialogueNode.
- Adds a message field.
- Adds an output.
- Returns the node from GetNextNode.

The node holds the message and the manager will handle writing it to the Console.

### Update DialogueManager

Open `DialogueManager.cs` and find the node checks inside `ProcessNode()`.

Add this before the EndNode check:

```csharp
else if (currentNode is LogMessageNode logMessageNode)
{
    Debug.Log(logMessageNode.message);

    currentNode = logMessageNode.GetNextNode();
    ProcessNode();
}
```

This will writes the message and get the connected node then continue..

### Use the Custom Node

1. Open a dialogue graph.
2. Right click and add a **LogMessageNode**.
3. Enter a message.
4. Connect the graph:

`Start > LogMessage > Monologue > End`

5. Enter Play Mode.
6. Check the message shows in the Console.
7. Check the dialogue continues to the next node.

## Showing Dialogue Fields on Custom Nodes

DialogueNode uses these shared fields:

```csharp
public string speakerName;
public string dialogueText;
public AudioClip audioClip;
public AnimationClip animationClip;
public string targetAnimationObjectName;
```

Action nodes do not normally need these fields, so they are hidden in the graph by default.

DialogueNode hides them using:

```csharp
public virtual bool ShowDialogueFields => false;
```

If your node needs the dialogue fields you can add this:

```csharp
public override bool ShowDialogueFields => true;
```

MonologueNode and ChoiceNode use this override.

### Shared Graph Editor

`Editor/DialogueNodeEditor.cs` handles the shared fields displaying on the nodes.

Nodes inheriting DialogueNode use this.

New serialized fields appear normally. The editor only filters out the shared dialogue fields when ShowDialogueFields is set to false.

If you create a separate custom graph editor:

- Keep the editor script in the **Editor** folder.
- Keep the field filtering if required.
- Use xNode's drawing methods to show connection ports.

## GameObject Names

Some nodes find objects using:

```csharp
DialogueUtilities.FindObjectByName()
```

This will returns the first GameObject it finds with a matching name. It can include disabled objects and loaded assets.

When setting up object based nodes:

- Use single use GameObject names.
- Match the spelling and capitalisation exactly.
- Update the node if you rename the object.

Most action nodes need a target. A missing object can show an error and stop the graph from continuing.

## Troubleshooting

| Problem | What to check |
| - | - |
| Dialogue does not start | Trigger enabled, graph and manager assigned, correct trigger option and connected Start node |
| Buttons do not respond | EventSystem, input module, Canvas interaction and Next button event |
| A choice does not continue | The selected choice output is connected |
| Object not found | Exact object name and if the target exists |
| Null-reference error | UI references, scene object names and node connections |
| Audio does not play | Audio clip, active camera, AudioListener and audio settings |
| Animation does not play | Legacy-compatible clip and correct target object |
| Scene does not load | The scene in the active build profile is the correct scene index |
| Custom node does nothing | Its handling has been added to DialogueManager |
| Dialogue fields show unexpectedly | ShowDialogueFields setting and any custom graph editor |