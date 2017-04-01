using System;
using InControl;
using UnityEngine;

public class MyPlayerAction : PlayerActionSet {
	
	//movement head
	public PlayerAction lookLeft;
	public PlayerAction lookRight;
	public PlayerAction lookUp;
	public PlayerAction lookDown;
	public PlayerTwoAxisAction lookVector2;

	public PlayerAction lookZoomForward;
	public PlayerAction	lookZoomBackward;

	//movement player
	public PlayerAction moveLeft;
	public PlayerAction moveRight;
	public PlayerAction moveForward;
	public PlayerAction moveBackward;
	public PlayerTwoAxisAction moveVector2;

	//actions
	public PlayerAction jump;
	public PlayerAction interact1;
	public PlayerAction interact2;
	public PlayerAction menu;
	//public PlayerAction back;

	public MyPlayerAction () {
		lookLeft = CreatePlayerAction("look left");
		lookRight = CreatePlayerAction("look rigth");
		lookUp = CreatePlayerAction("look up");
		lookDown = CreatePlayerAction("look down");
		lookVector2 = CreateTwoAxisPlayerAction( lookLeft, lookRight, lookDown, lookUp);

		lookZoomForward = CreatePlayerAction("look zoom forward");
		lookZoomBackward = CreatePlayerAction("look zoom backward");

		moveLeft = CreatePlayerAction("move left");
		moveRight = CreatePlayerAction("move right");
		moveForward = CreatePlayerAction("move forward");
		moveBackward = CreatePlayerAction("move backward");
		moveVector2 = CreateTwoAxisPlayerAction( moveLeft, moveRight, moveBackward, moveForward);

		jump = CreatePlayerAction("jump");
		interact1 = CreatePlayerAction("interact1");
		interact2 = CreatePlayerAction("interact2");
		menu = CreatePlayerAction("menu");
		//back = CreatePlayerAction("back");
	}

	//bind keys to input types
	public static MyPlayerAction CreateWithDefaultBindings() {

		var myPlayerAction = new MyPlayerAction();

		#region keyboard
		myPlayerAction.lookLeft.AddDefaultBinding( Mouse.NegativeX );
		myPlayerAction.lookRight.AddDefaultBinding( Mouse.PositiveX );
		myPlayerAction.lookUp.AddDefaultBinding( Mouse.PositiveY );
		myPlayerAction.lookDown.AddDefaultBinding( Mouse.NegativeY );

		myPlayerAction.lookZoomForward.AddDefaultBinding(Mouse.PositiveScrollWheel);
		myPlayerAction.lookZoomBackward.AddDefaultBinding(Mouse.NegativeScrollWheel);


		myPlayerAction.moveLeft.AddDefaultBinding( Key.A );
		myPlayerAction.moveRight.AddDefaultBinding( Key.D );
		myPlayerAction.moveForward.AddDefaultBinding( Key.W );
		myPlayerAction.moveBackward.AddDefaultBinding( Key.S );
		//arrow keys
		myPlayerAction.moveLeft.AddDefaultBinding( Key.LeftArrow );
		myPlayerAction.moveRight.AddDefaultBinding( Key.RightArrow );
		myPlayerAction.moveForward.AddDefaultBinding( Key.UpArrow );
		myPlayerAction.moveBackward.AddDefaultBinding( Key.DownArrow );

		myPlayerAction.jump.AddDefaultBinding( Key.Space );
		myPlayerAction.interact1.AddDefaultBinding( Key.Q );
		myPlayerAction.interact2.AddDefaultBinding( Key.E );
		myPlayerAction.menu.AddDefaultBinding( Key.Escape );
		//myPlayerAction.back.AddDefaultBinding( Key.Escape );
		#endregion

		#region gamepad
		myPlayerAction.lookLeft.AddDefaultBinding( InputControlType.RightStickLeft );
		myPlayerAction.lookRight.AddDefaultBinding( InputControlType.RightStickRight );
		myPlayerAction.lookUp.AddDefaultBinding( InputControlType.RightStickUp );
		myPlayerAction.lookDown.AddDefaultBinding( InputControlType.RightStickDown );

		myPlayerAction.lookZoomForward.AddDefaultBinding( InputControlType.RightTrigger );
		myPlayerAction.lookZoomBackward.AddDefaultBinding( InputControlType.RightBumper );

		myPlayerAction.moveLeft.AddDefaultBinding( InputControlType.LeftStickLeft );
		myPlayerAction.moveRight.AddDefaultBinding( InputControlType.LeftStickRight );
		myPlayerAction.moveForward.AddDefaultBinding( InputControlType.LeftStickUp );
		myPlayerAction.moveBackward.AddDefaultBinding( InputControlType.LeftStickDown );
		//arrow keys
		myPlayerAction.moveLeft.AddDefaultBinding( InputControlType.DPadLeft );
		myPlayerAction.moveRight.AddDefaultBinding( InputControlType.DPadRight );
		myPlayerAction.moveForward.AddDefaultBinding( InputControlType.DPadUp );
		myPlayerAction.moveBackward.AddDefaultBinding( InputControlType.DPadDown );

		myPlayerAction.jump.AddDefaultBinding( InputControlType.LeftBumper );
		myPlayerAction.jump.AddDefaultBinding( InputControlType.LeftTrigger );
		myPlayerAction.jump.AddDefaultBinding( InputControlType.LeftStickButton );
		myPlayerAction.interact1.AddDefaultBinding( InputControlType.Action1);
		myPlayerAction.interact2.AddDefaultBinding( InputControlType.Action2);
		myPlayerAction.menu.AddDefaultBinding( InputControlType.Command );
		//myPlayerAction.back.AddDefaultBinding( InputControlType.Action3 );
		#endregion

		//set options
		myPlayerAction.ListenOptions.IncludeUnknownControllers = true;
		//playerActions.ListenOptions.MaxAllowedBindings = 5;
		myPlayerAction.ListenOptions.IncludeMouseButtons = true;

		myPlayerAction.ListenOptions.OnBindingFound = ( action, binding ) =>
		{
			if (binding == new KeyBindingSource( Key.Escape ))
			{
				action.StopListeningForBinding();
				return false;
			}
			return true;
		};
		
		myPlayerAction.ListenOptions.OnBindingAdded += ( action, binding ) =>
		{
			Debug.Log( "Binding added... " + binding.DeviceName + ": " + binding.Name );
		};
		
		myPlayerAction.ListenOptions.OnBindingRejected += ( action, binding, reason ) =>
		{
			Debug.Log( "Binding rejected... " + reason );
		};
		
		return myPlayerAction;
	}
}