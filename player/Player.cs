using EffectSystemPrototype;
using Godot;
using System;
using System.Collections.Generic;
using System.Security;

public partial class Player : Node2D
{
	[Export]
	Area2D hitbox;

	const float Speed = 200;

	const int StartLife = 50;

	public override void _Ready()
	{
		SetupProperties();
		SetupEffects();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move((float)delta);
	}

	void Move(float delta)
	{
		var vec = Input.GetVector("left", "right", "up", "down");
		Position += vec * Speed * delta;
		Effects.System.Inputs["walking"] = vec.Length() != 0;
	}

	void SetupEffects()
	{
		Func<InputVector, double> damageFunc = (inputs) =>
		{
			double damage = inputs.PropertyValue("damage");
			return -damage;
		};

		var damageEffect = new InputEffect("health", damageFunc, "add");
		Effects.System.AddEffect(damageEffect);
    }

	void SetupProperties()
	{
        PropertyConfig damageConfig = new("damage");
        damageConfig.Apply(Effects.System);

        PropertyConfig healthConfig = new("health");
        healthConfig.StartValue = StartLife;
        healthConfig.MinValue = 0;
        healthConfig.MaxValue = 100;
		healthConfig.IsPermanent = true;
        healthConfig.Apply(Effects.System);

    }
}
