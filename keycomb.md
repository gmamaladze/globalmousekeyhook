#Detecting Key Combinations and Seuqnces

##Quickstart

Assuming your goal is to detect whenever some key combination, shortcut is pressed and perform some action as a response. Use class `Combination` to describe key combinations.
```csharp
var undo = Combination.FromString("Control+Z");
var fullScreen = Combination.FromString("Shit+Alt+Enter");
```


