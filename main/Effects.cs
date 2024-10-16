using Godot;
using System;

public partial class Effects : Node
{
    public static EffectSystemPrototype.EffectSystem System;

    public Effects()
    {
        System = new EffectSystemPrototype.EffectSystem();
    }
}
