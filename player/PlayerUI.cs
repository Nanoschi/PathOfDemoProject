using Godot;
using System;

public partial class PlayerUI : Control
{
	[Export]
	ProgressBar HealthBar;

    [Export]
    Label HealthLabel;

    public override void _Ready()
	{
		SetHealth(100);
		ESManager.Instance.ProcessedSystem += UpdateUI;
	}

	
	public override void _Process(double delta)
	{
		
	}

	private void SetHealth(double value)
	{
		HealthBar.Value = value;
		HealthLabel.Text = value.ToString("N1");
	}

	private void UpdateUI()
	{
        var health = Effects.System.Results["health"];
        SetHealth(health);
    }
}
