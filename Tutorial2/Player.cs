using Godot;
using System;

public class Player : Node2D
{
	PackedScene bulletScene;

	public override void _Ready()
	{
		bulletScene = GD.Load<PackedScene>("res://Bullet.tscn");
	}

	public override void _Process(float delta)
	{
		// move in response to arrow keys
		float speed = 150; // pixels per second
		float moveAmount = speed * delta;
		Vector2 moveVector = new Vector2(0,0);
		if (Input.IsKeyPressed((int)KeyList.Up)){
			moveVector.y = -1;
		}
		if (Input.IsKeyPressed((int)KeyList.Down)){
			moveVector.y = 1;
		}
		if (Input.IsKeyPressed((int)KeyList.Left)){
			moveVector.x = -1;
		}
		if (Input.IsKeyPressed((int)KeyList.Right)){
			moveVector.x = 1;
		}
		Position += moveVector.Normalized() * moveAmount;

		// face the mouse
		Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent ){
			if (mouseEvent.ButtonIndex == (int)ButtonList.Left && mouseEvent.Pressed){
				Bullet bullet = (Bullet)bulletScene.Instance();
				bullet.Position = Position;
				bullet.Rotation = Rotation;
				GetParent().AddChild(bullet);
				GetTree().SetInputAsHandled();
			}
		}
	}
}
