using Godot;
using System;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;
using MasterofElements.scripts.singletons.score;

public partial class Slime : CharacterBody2D
{
	[Export] public float MoveSpeed = 2500;
	[Export] public int Hp = 100;
	[Export] public int Score = 10;
	[Export] public Facing Direction = Facing.Right;
	

	private AutoLoader _autoLoader;
	
	private Sprite2D _slimeSprite;
	private Sprite2D _explosion;
	private RayCast2D _rayCast2D;
	private Area2D _hitBox;
	private Facing _facing;
	
	private AnimationPlayer _animationPlayer;
	
	
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public override void _Ready()
	{
		_autoLoader = new AutoLoader(this);
		_rayCast2D = GetNode<RayCast2D>("RayCast2D");
		_hitBox = GetNode<Area2D>("HitBox");
		_slimeSprite = GetNode<Sprite2D>("Node2D/SlimeSprite");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_explosion = GetNode<Sprite2D>("Node2D/SlimeExplosion");
		_facing = Facing.Right;
		if (Direction == Facing.Left)
		{
			FlipDirection();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity.Y = _gravity * (float)delta;
		}
		else
		{
			GD.Print((int)_facing);
			velocity.X = (int)_facing * MoveSpeed *(float)delta;
		}

		Velocity = velocity;
		MoveAndSlide();
		
		if (IsOnFloor() && (IsOnWall() || !_rayCast2D.IsColliding()))
		{
			FlipDirection();
		}
	}

	private void FlipDirection()
	{
		_slimeSprite.FlipH = !_slimeSprite.FlipH;
		_rayCast2D.Position = new Vector2(_rayCast2D.Position.X * -1, _rayCast2D.Position.Y);

		if (_facing == Facing.Left)
		{
			_facing = Facing.Right;
		}
		else
		{
			_facing = Facing.Left;
		}
	}

	private void Die()
	{
		_slimeSprite.Hide();
		_explosion.Show();
		_autoLoader.AudioService.PlaySfx("explosion.mp3",this);
		_autoLoader.ScoreService.OnScoreEvent(Score, ScoreType.EnemyDefeated);
		_animationPlayer.Play("death");
	}

	private void OnHitBoxEntered(Area2D area)
	{
		
	}
}
