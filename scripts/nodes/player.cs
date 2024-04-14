using Godot;
using System;
using MasterofElements.scripts.models;
using MasterofElements.scripts.singletons;

public partial class player : CharacterBody2D
{
	private AutoLoader _autoLoader;
	public const float Speed = 20000.0f;
	public const float JumpVelocity = -400.0f;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private Godot.Sprite2D _playerSprite2D;
	private AudioStreamPlayer2D _audioStreamPlayer2D;
	private AnimationPlayer _animationPlayer;
	private PlayerState _playerState;
	private bool _animationFinish = true;

	public override void _Ready()
	{
		base._Ready();
		_autoLoader = new AutoLoader(this);
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_playerSprite2D = GetNode<Godot.Sprite2D>("PlayerSprite2D");
		_audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		MovePlayer(delta);
		MoveAndSlide();
		HandlePlayerAnimation();
	}

	private void MovePlayer(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y += JumpVelocity;
		}

		Vector2 direction = Vector2.Zero;
		if (Input.IsActionPressed("left"))
		{
			_playerSprite2D.FlipH = true;
			direction = Vector2.Left;
		}

		if (Input.IsActionPressed("right"))
		{
			_playerSprite2D.FlipH = false;
			direction = Vector2.Right;
		}
		
		velocity.X = direction.X * Speed * (float)delta;
		
		Velocity = velocity;
		
	}
	
	private void HandlePlayerAnimation()
	{
		if (_playerState == PlayerState.Hurt)
		{
			return;
		}

		if (_animationFinish)
		{
			if (IsOnFloor())
			{
				if (Velocity.X == 0)
				{
					SetPlayerState(PlayerState.Idle);
				}
				else
				{
					SetPlayerState(PlayerState.Walk);
				}
			}

			if (Input.IsActionJustPressed("summon_air"))
			{
				SetPlayerState(PlayerState.SummonAir);
				_animationFinish = false;
			}
			if (Input.IsActionJustPressed("summon_water"))
			{
				SetPlayerState(PlayerState.SummonWater);
				_animationFinish = false;
			}
			if (Input.IsActionJustPressed("summon_fire"))
			{
				SetPlayerState(PlayerState.SummonFire);
				_animationFinish = false;
			}
			if (Input.IsActionJustPressed("summon_earth"))
			{
				SetPlayerState(PlayerState.SummonEarth);
				_animationFinish = false;
			}
		}
		else
		{
			if (Velocity.Y > 0)
			{
				SetPlayerState(PlayerState.Fall);
				_animationFinish = false;
			}
			else
			{
				SetPlayerState(PlayerState.Jump);
				_animationFinish = false;
			}
		}
	}

	private void SetPlayerState(PlayerState newPlayerState)
	{
		if (newPlayerState == _playerState)
		{
			
		}

		if (_playerState == PlayerState.Fall)
		{
			if (newPlayerState == PlayerState.Idle || newPlayerState == PlayerState.Walk)
			{
				
			}
		}
		

		_playerState = newPlayerState;

		switch (_playerState)
		{
			case PlayerState.Idle:
				_animationPlayer.Play("idle");
				break;
			case PlayerState.Walk:
				_animationPlayer.Play("walk");
				break;
			case PlayerState.Jump:
				_animationPlayer.Play("jump");
				//_autoLoader.AudioService.PlaySFX("jump.mp3", this);
				break;
			case PlayerState.Fall:
				_animationPlayer.Play("fall");
				break;
			case PlayerState.Death:
				_animationPlayer.Play("death");
				break;
			case PlayerState.SummonEarth:
				_animationPlayer.Play("summon_earth");
				break;
			case PlayerState.SummonAir:
				_animationPlayer.Play("summon_air");
				break;
			case PlayerState.SummonFire:
				_animationPlayer.Play("summon_fire");
				break;
			case PlayerState.SummonWater:
				_animationPlayer.Play("summon_water");
				
				break;
			case PlayerState.Hurt:
				break;
		}
	}
	
	public PlayerState PlayerState
	{
		get => _playerState;
		set => _playerState = value;
	}

	public void PlayWalkSound()
	{
		//_autoLoader.AudioService.PlaySFX("Walk.mp3", this);
	}

	private void OnAnimationFinished()
	{
		_animationFinish = true;
	}
}
