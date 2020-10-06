using Godot;
using System;

public class Bullet : Node2D
{
	public float Range = 400;
	
	private float distanceTravelled = 0;

	public override void _Ready()
	{
		var area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered",this,"OnCollision");
		area.Connect("body_entered",this,"OnCollision");
	}

	public override void _Process(float delta)
	{
		// move forward
		float speed = 400; // in pixels per second
		float moveAmount = speed * delta; // in pixels
		Position += Transform.x.Normalized() * moveAmount;
		distanceTravelled += moveAmount;
		if (distanceTravelled > Range)
			QueueFree();
	}

	private void OnCollision(Node with){
		GD.Print("Bullet collided!");
		QueueFree();
	}
}
