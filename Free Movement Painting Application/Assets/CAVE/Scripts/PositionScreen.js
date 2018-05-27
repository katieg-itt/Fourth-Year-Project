/// IMPORT STATEMENTS
import WindowHandler; // Moves / undecorates windows.

// Screen information.
var ScreenWidth : int[];	// index 0 = development, index 1 = production
var ScreenHeight : int[];	// index 0 = development, index 1 = production
var WindowName : String;

// coordinates to place the screen.
var ScreenCoordinates : Vector2[]; // index 0 = development, index 1 = production

// development or production
var index : int = 0;	// index into ScreenWidth, ScreenHeight, and ScreenCoordinates

// should the window be undecorated
var undecorate : boolean = false;

// Holds on to the handle of the window when we get it.
private var windowHandle;


// The main method of the monitor setup. Call this when the client has successfully connected.
function Start()
{
	
	// Grab the handle of the current window.
	windowHandle = GetForegroundWindow();
	
	// Set the screen resolution.
	Screen.SetResolution(ScreenWidth[index], ScreenHeight[index], false);
	
	// Wait for one frame before undecorating to make sure we don't break everything.
	yield;
		
	// place it in the right location if we're not in the editor.
	if(!Application.isEditor) {
		if(undecorate) {
			UndecorateAndPlace(ScreenCoordinates[index].x, ScreenCoordinates[index].y, ScreenWidth[index], ScreenHeight[index]);
		}
		else {
			PlaceWindow(ScreenCoordinates[index].x, ScreenCoordinates[index].y, ScreenWidth[index], ScreenHeight[index]);
		}
		
		// Set the screen name for the window that has focus.
		RenameWindow(windowHandle, WindowName);
		
		// Minimize and maximize so that after the client windows are made, regain focus to server.
//		RegainFocus();
	}
	
}

function Update() {
}

// Function that causes the window to minimize and then restore to original position.
// Use this for your non-dedicated server and AFTER you undecorate it.
function RegainFocus()
{
	yield WaitForSeconds(1);
	
	// Minimize the window.
	MinWindow(windowHandle);
	
	// Wait until the next update to ensure we don't break something by doing this all at once.
	yield;
	
	// Restore the window to its original size / location and regain focus.
	RestoreWindow(windowHandle);
	
	// Wait again to be safe.
	yield;
}