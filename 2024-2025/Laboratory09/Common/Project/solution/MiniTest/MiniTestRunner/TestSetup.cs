using System.Reflection;

namespace MiniTestRunner;

public record TestSetup(MethodInfo MethodInfo)
{
    private Action? Method { get; set; }

    private Action Bind(object instance)
    {
        return (Action)Delegate.CreateDelegate(typeof(Action), instance, MethodInfo);
    }
    
    public void Run(object instance)
    {
        Method ??= Bind(instance);
        Method();
    }
}