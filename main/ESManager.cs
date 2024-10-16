using Godot;
using System;

public partial class ESManager : Node
{
    public static ESManager Instance;
    [Signal]
    public delegate void ProcessedSystemEventHandler();

    public ESManager()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        InitInputs();
        Effects.System.Process();
    }

    public override void _Process(double delta)
    {
        UpdateInputs();
        Effects.System.Process();
        EmitSignal(SignalName.ProcessedSystem);
    }

    private void InitInputs()
    {
        Effects.System.Inputs.TryAddValue("time", 0.0);
        Effects.System.Inputs.TryAddValue("delta_time", 0.0);
    }

    private void UpdateInputs()
    {
        var time = (double)Time.GetTicksMsec() / 1000.0;
        var inputTime = (double)Effects.System.Inputs["time"];
        Effects.System.Inputs.SetValue("time", time);
        Effects.System.Inputs.SetValue("delta_time", time - inputTime);
    }
}
