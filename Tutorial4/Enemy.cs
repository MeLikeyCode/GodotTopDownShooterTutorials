using Godot;

public class Enemy : Node2D
{
	private Timer timer;
	private int health = 5;

	public void Damage(int amount){
		health -= amount;
		if (health < 0){
			QueueFree();
		}
	}

	public override void _Ready()
	{
		var area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered",this,nameof(OnCollision));
		area.Connect("area_exited",this,nameof(OnCollisionNoMore));

		timer = GetNode<Timer>("Timer");
		timer.Connect("timeout",this,nameof(OnTimerTimeout));
	}

	public override void _Process(float delta)
	{
		// move towards player
		var player = GetNode<Player>("../Player");
		float speed = 80; // in pixels per second
		float moveAmount = speed * delta;
		Vector2 moveDirection = (player.Position - Position).Normalized();
		Position += moveDirection * moveAmount;
	}

	private void OnCollision(Area2D with){
		if (with.GetParent() is Player player){
			timer.Start(1);
		}
	}

	private void OnCollisionNoMore(Area2D with){
		if (with.GetParent() is Player player){
			timer.Stop();
		}
	}

	private void OnTimerTimeout(){
		var player = GetNode<Player>("../Player");
		if (player != null){
			player.Health -= 2;
			GD.Print("Enemy hurt player");
		}
	}
}
