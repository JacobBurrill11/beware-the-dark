using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public float SPEED = 5f;
	[Export] public float JUMPVELOCITY = 5f;
	[Export] public float GRAVITY = 8.19f;

	public float rotation_angle = 0.0f;

	public AnimationPlayer anim;

	public GpuParticles3D dust;

	public Area3D EnemyHitArea;

	public Vector3 velocity;

	public OmniLight3D light;

	public LevelManager levelmanager;

	private Vector3 respawnPosition = new Vector3(-16f, 1.1f, -14f); // Example: Set your desired respawn location
    private float fallThreshold = -10.0f;

	



	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		EnemyHitArea = GetNode<Area3D>("Enemy_hit_area");
		dust = GetNode<GpuParticles3D>("Dust");
		light = GetNode<OmniLight3D>("PlayerLight");
		levelmanager = GetTree().Root.GetNode<LevelManager>("LevelManager");
		 if (GetNodeOrNull<Node3D>("SpawnPoint") != null)
        {
            respawnPosition = GlobalPosition;
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocity = Velocity;

		Vector2 input = Input.GetVector("left","right","up","down");

		Basis camBasis = GetViewport().GetCamera3D().Basis;
		
		Vector3 direction = camBasis * new Vector3(input.X, 0, input.Y);

		if(!IsOnFloor()){
			velocity.Y -= GRAVITY *(float)delta;
			EnemyHitArea.Monitoring = true;
			
		}
		else{
			EnemyHitArea.Monitoring = false;
			if(Input.IsActionJustPressed("jump")){
				velocity.Y = JUMPVELOCITY;
				Scale = new Vector3(0.5f, 1.5f, 0.5f);
				
			}
		}
		Scale = Scale.Lerp(new Vector3(1.0f,1.0f,1.0f), (float)delta*4.0f);

		velocity.X = direction.X * SPEED;
		velocity.Z = direction.Z * SPEED;

		Velocity = velocity;

		if(new Vector2(Velocity.X, Velocity.Z).Length() >0){
			rotation_angle = new Vector2(Velocity.Z, Velocity.X).Angle();
			Vector3 rot = Rotation;

			rot.Y = (float)Mathf.LerpAngle(rot.Y,rotation_angle, 10 * delta);

			Rotation = rot;
		}

		light.OmniRange = levelmanager.lightLevel;

		if (GlobalPosition.Y < -10 ){
			Respawn();
		}

		MoveAndSlide();
		Animate();
		
	}

	public void Animate(){
		if (IsOnFloor()){
			if (new Vector2(Velocity.X, Velocity.Z).Length() >0){
				anim.Play("walk");
				dust.Emitting = true;
			}
			else{
				anim.Play("idle");
				dust.Emitting = false;
			}
		}else{
			dust.Emitting = false;
			anim.Play("jump");
		}
	}

	private void Respawn()
    {
        // Reset the player's position and velocity
        GlobalPosition = respawnPosition;
        Velocity = Vector3.Zero;
	}
	public void HitEnemy(Ghost body){
		if (body.HasMethod("Die")){
			body.Die();
		}
		velocity.Y = JUMPVELOCITY/2.0f;
		Velocity = velocity;
	}


}

