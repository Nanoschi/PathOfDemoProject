using EffectSystemPrototype;
using Godot;
using System;

public partial class BleedField : Node2D
{

	[Export]
	Area2D Area { get; set; }

    InputEffect BleedEffect;

    const double StandDamagePerSecond = 2;
    const double WalkDamagePerSecond = 10;
    const double EffectTime = 5;

    public override void _Ready()
	{
        Area.AreaEntered += OnAreaEntered;
        CreateEffect();
    }

	
	public override void _Process(double delta)
	{
	}

    private void OnAreaEntered(Area2D area)
    {
        if (area.IsInGroup("player"))
        {
            var currentTime = Time.GetTicksMsec() / 1000.0;
            Effects.System.Thresholds.AddEffect(BleedEffect, "time", currentTime + 5, RemoveCondition.Greater);
            Effects.System.AddEffect(BleedEffect);
        }
    }

    private void CreateEffect()
    {
        Func<InputVector, double> bleedFunc = (inputs) =>
        {
            bool walking = (bool)inputs["walking"];
            if (walking)
            {
                return (double)inputs["delta_time"] * WalkDamagePerSecond;
            }
            else
            {
                return (double)inputs["delta_time"] * StandDamagePerSecond;
            }
        };

        BleedEffect = new InputEffect("damage", bleedFunc, "add");
    }
}
