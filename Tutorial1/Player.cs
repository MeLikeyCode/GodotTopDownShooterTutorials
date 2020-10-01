using Godot;
using System;

public class Player : Node2D
{
	public override void _Process(float delta)
	{
		// move in response to arrow keys
		float speed = 150; // pixels per second
		float moveAmount = speed * delta;
		Vector2 moveVector = new Vector2(0,0);
		if (Input.IsKeyPressed((int)KeyList.Up)){
			moveVector.y = -moveAmount;
		}
		if (Input.IsKeyPressed((int)KeyList.Down)){
			moveVector.y = moveAmount;
		}
		if (Input.IsKeyPressed((int)KeyList.Left)){
			moveVector.x = -moveAmount;
		}
		if (Input.IsKeyPressed((int)KeyList.Right)){
			moveVector.x = moveAmount;
		}
		Position += moveVector;

		// face the mouse
		Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();
	}
}
