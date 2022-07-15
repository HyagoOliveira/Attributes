# Attributes

* Property attributes for Unity fields
* Unity minimum version: **2019.1**
* Current version: **1.2.0**
* Licence: **MIT**

## Summary

## How To Use

Just use one of the following attributes:

* **AssetsOnly**: only select assets references.
* **Required**: use it on strings, exposed and object reference types. If the value is null or empty, a error message will be displayed on Inspector bellow the given attribute.
* **Tag**: use it only with a string fields. It'll replace the string field by a tag popup.
* **DisableInPlayMode**: use this to prevent users from editing a property when in play mode.

```csharp
using UnityEngine;
using ActionCode.Attributes;

public sealed class TestBehaviour : MonoBehaviour
{
    [AssetsOnly(typeof(GameObject))] public GameObject player;
    [Required] public string playerId;
    [Tag] public string playerTag = "Player";
}
```

![Attributes](/Documentation~/attributes-simple.gif)

* **ShowIf**: use it to show properties based on the current state of the object.

```csharp
public sealed class TestBehaviour : MonoBehaviour
{
    [Range(0, 20)] public int powerLevel;
    [ShowIf("powerLevel", LogicalOperatorType.GreaterOrEqual, 10)]
    public string powerLevelName = "Super Power";
}
```

![Show If Attribute Showcase](/Documentation~/attributes-show-if.gif)

## Installation

### Using the Package Registry Server

Follow the instructions inside [here](https://cutt.ly/ukvj1c8) and the package **ActionCode-Attributes** will be available for you to install using the **Package Manager** windows.

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set. 

Use the **Package Manager** "Add package from git URL..." feature or add manually this line inside `dependencies` attribute: 

```json
"com.actioncode.attributes":"https://bitbucket.org/nostgameteam/attributes.git"
```

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[BitBucket](https://bitbucket.org/HyagoGow/) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>