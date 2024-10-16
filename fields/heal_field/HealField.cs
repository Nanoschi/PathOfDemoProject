using EffectSystemPrototype;
using Godot;
using System;

public partial class HealField : Node2D
{
	[Export]
	Area2D HealArea;

	InputEffect HealEffect;

	const double HealPerSecond = -5;
	
	public override void _Ready()
	{
		HealArea.AreaEntered += OnAreaEntered;
		HealArea.AreaExited += OnAreaExited;
		CreateEffect();
    }

	
	public override void _Process(double delta)
	{
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("player"))
		{
			Effects.System.AddEffect(HealEffect);
		}
	}

    private void OnAreaExited(Area2D area)
    {
        if (area.IsInGroup("player"))
        {
            Effects.System.RemoveEffect(HealEffect);
        }
    }

	private void CreateEffect()
	{
		Func<InputVector, double> healFunc = (inputs) =>
		{
			return (double)inputs["delta_time"] * HealPerSecond;
		};

		HealEffect = new InputEffect("damage", healFunc, "add");
	}
}
