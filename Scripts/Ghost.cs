using Godot;
using System;
using System.Collections;

public partial class Ghost : CharacterBody3D
{
    
    [Export] public float SPEED = 2.0f;
    [Export] public float ATTACKRANGE = 10.0f;
    [Export] public float JumpVelocity = 2.0f;

    [Export] public AnimationPlayer anim;

    public Node3D target; 

    public float GRAVITY = -9.85f;

    public Vector3 velocity;

    public float rotation_angle = 0.0f;


    public enum State{
        IDLE,
        ATTACK,
        STRUGGLE,
        DIE
    }

    public State CurrentState;
    public override void _Ready()
    {
        CurrentState = State.IDLE;
        target = GetParent().GetParent().GetNode<Node3D>("character-female-a");
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        switch(CurrentState){
            case State.IDLE:
                UpdateIdle();
                break;
            case State.ATTACK:
                UpdateAttack(delta);
                break;
            case State.DIE:
                UpdateDie();
                break;

        }

        Animate();
    }
    private void UpdateIdle(){
        if (Position.DistanceTo(target.Position) < ATTACKRANGE){
            CurrentState = State.ATTACK;
        }
    }

    private void UpdateAttack(double delta){
        if (Position.DistanceTo(target.Position) > 1.25f*ATTACKRANGE){
            CurrentState = State.IDLE;
        }
        Vector3 t = target.Position;
        t.Y = Position.Y;
        Vector3 direction = Position.DirectionTo(t);
        velocity = Velocity;
        velocity.X= direction.X*SPEED;
        velocity.Z = direction.Z * SPEED;

        Velocity = velocity;

        if(new Vector2(Velocity.X, Velocity.Z).Length() >0){
			rotation_angle = new Vector2(Velocity.Z, Velocity.X).Angle();
			Vector3 rot = Rotation;

			rot.Y = (float)Mathf.LerpAngle(rot.Y,rotation_angle, 10 * delta);

			Rotation = rot;
		}
        MoveAndSlide();
        CheckForPlayer();


    }

    public void CheckForPlayer(){
        for(int i =0; i < GetSlideCollisionCount();i++){
            KinematicCollision3D c = GetSlideCollision(i);
            GodotObject co = c.GetCollider();
            if(co is Player){
                if(((Player)co).IsOnFloor()){
                    GetTree().ReloadCurrentScene();
                }
            }
        }
    }

    public void Animate(){
        if (Velocity.Length()>0.0){
		
		    anim.Play("walk");
        }
			
		else{
			anim.Play("idle");
		}
    }

    public void UpdateDie(){
        Tween tw = CreateTween();
        tw.TweenProperty(this, "scale", new Vector3(1.5f, 0.25f, 1.5f), 0.2f);
        tw.TweenCallback(Callable.From(() => QueueFree()));
    }


    public void Die(){
        CurrentState = State.DIE;
    }
}
