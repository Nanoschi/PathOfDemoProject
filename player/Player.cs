using EffectSystemPrototype;
using Godot;
using System;
using System.Collections.Generic;
using System.Security;

public partial class Player : Node2D
{
	[Export]
	Area2D hitbox;

	const float Speed = 140;

	const int StartLife = 100;
	const int Regen = -1;

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

		Action<InputVector, Dictionary<string, object>> regenCtor = (inputs, data) =>
		{
			var time = Time.GetTicksMsec();
			data["last_apply"] = time;
		};

		Func<InputVector, Dictionary<string, object>, double> regenFunc = (inputs, data) =>
		{
			var time = Time.GetTicksMsec();
			var last_apply = (ulong)data["last_apply"];
			var delta = (time - last_apply) / 1000.0;
			data["last_apply"] = time;
			return delta * -Regen;
		};

		var regenEffect = new DataEffect("damage", regenFunc, "add");
		regenEffect.Constructor = regenCtor;
		Effects.System.AddEffect(regenEffect);
    }

	void SetupProperties()
	{
        PropertyConfig damageConfig = new("damage");
        damageConfig.Apply(Effects.System);

        PropertyConfig healConfig = new("heal");
        damageConfig.Apply(Effects.System);

        PropertyConfig healthConfig = new("health");
        healthConfig.StartValue = StartLife;
        healthConfig.MinValue = 0;
        healthConfig.MaxValue = 100;
		healthConfig.IsPermanent = true;
        healthConfig.Apply(Effects.System);

    }
}
