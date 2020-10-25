# Screen Pixel Ruler
An open source and free screen pixel ruler based on C#/.Net Core 3.1 that is a clone of the application *MioPlanet PixelRuler*.

# Background
I was using *MioPlanet PixelRuler* for pixel precise measuring of user interface spacing and found that it was lacking some quality of life features. The utility is quite dated and I needed the ability to freeze the ruler measurements, shift the start of the ruler to the current cursor position and reverse the ruler notches.

I decided that I should write my own one based on the .NET framework and open-source it. The [first version](https://github.com/Cossey/screenpixelruler) was written in VB.Net and based on the .NET Framework 4.5. This new version is rewritten from the ground up in C# and based on .NET Core 3.1.

## How to Use
* Click and Drag the ruler to move it. 
* Left clicking on the ruler (or Ctrl + R) will rotate it either horizontally or vertically.
* Middle clicking on the ruler (or Ctrl + E) will flip the ruler lines to be on the opposite side of the ruler.
* Right clicking on the ruler will open a context menu with About and Exit (Ctrl + X) options.
* Ctrl + S will shift the Rulers edge to the position of the mouse cursor.
* Ctrl + F will freeze the Cursor Counter and Ruler size until toggled off (Ctrl + F).

> Keyboard Shortcuts work only when the Ruler has focus
