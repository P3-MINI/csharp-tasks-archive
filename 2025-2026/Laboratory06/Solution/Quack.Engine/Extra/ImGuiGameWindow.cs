using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Quack.Engine.Extra;

/// <summary>
/// A base game window that integrates ImGui, a synchronization context for async tasks, and abstract hooks for game logic.
/// </summary>
public abstract class ImGuiGameWindow : GameWindow
{
    private const string FontResource = "Quack.Engine.Resources.Fonts.NotoSansMono.ttf";
    private readonly GLSynchronizationContext _context;
    private bool IsLoaded { get; set; }
    private ImGuiController ImGuiController { get; set; } = null!;
    
    protected ImGuiGameWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
        : base(gameWindowSettings, nativeWindowSettings)
    {
        _context = new GLSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(_context);
    }

    protected sealed override void OnLoad()
    {
        base.OnLoad();
        ImGuiController = new ImGuiController(ClientSize.X, ClientSize.Y, FontResource);
        IsLoaded = true;
        OnWindowLoad();
    }

    protected sealed override void OnUnload()
    {
        base.OnUnload();
        OnWindowUnload();
        ImGuiController.Dispose();
        IsLoaded = false;
    }

    protected sealed override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        if (!IsLoaded) return;
        ImGuiController.OnWindowResized(ClientSize.X, ClientSize.Y);
        OnWindowResize(e);
    }

    protected sealed override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        double budget = 5.0;
        if (UpdateFrequency > 0)
        {
            budget = (1000.0 / UpdateFrequency) * 0.20;
        }

        _context.Update(budget);
        ImGuiController.Update((float)args.Time);
        OnUpdate(args.Time);
    }

    protected sealed override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        OnRender(args.Time);
    }

    protected sealed override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);
        ImGuiController.OnKey(e, true);
        OnWindowKeyDown(e);
    }

    protected sealed override void OnTextInput(TextInputEventArgs e)
    {
        base.OnTextInput(e);
        ImGuiController.OnPressedChar((char)e.Unicode);
        OnWindowTextInput(e);
    }

    protected sealed override void OnKeyUp(KeyboardKeyEventArgs e)
    {
        base.OnKeyUp(e);
        ImGuiController.OnKey(e, false);
        OnWindowKeyUp(e);
    }

    protected sealed override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);
        ImGuiController.OnMouseButton(e);
        OnWindowMouseDown(e);
    }

    protected sealed override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);
        ImGuiController.OnMouseButton(e);
        OnWindowMouseUp(e);
    }

    protected sealed override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);
        ImGuiController.OnMouseMove(e);
        OnWindowMouseMove(e);
    }

    protected sealed override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);
        ImGuiController.OnMouseScroll(e);
        OnWindowMouseWheel(e);
    }

    /// <summary>
    /// Renders the ImGui layout.
    /// </summary>
    protected virtual void RenderGui()
    {
        ImGuiController.Render();
    }
    
    /// <summary>
    /// Called when the window is loaded. Initialize your game resources here.
    /// </summary>
    protected abstract void OnWindowLoad();

    /// <summary>
    /// Called when the window is unloading. Dispose of your game resources here.
    /// </summary>
    protected abstract void OnWindowUnload();

    /// <summary>
    /// Called every frame to update game logic.
    /// </summary>
    /// <param name="time">Time elapsed since the last update in seconds.</param>
    protected abstract void OnUpdate(double time);

    /// <summary>
    /// Called every frame to render the game.
    /// </summary>
    /// <param name="time">Time elapsed since the last render in seconds.</param>
    protected abstract void OnRender(double time);

    /// <summary>
    /// Called when the window is resized.
    /// </summary>
    protected abstract void OnWindowResize(ResizeEventArgs e);
    
    protected virtual void OnWindowKeyDown(KeyboardKeyEventArgs e) { }
    protected virtual void OnWindowTextInput(TextInputEventArgs e) { }
    protected virtual void OnWindowKeyUp(KeyboardKeyEventArgs e) { }
    protected virtual void OnWindowMouseDown(MouseButtonEventArgs e) { }
    protected virtual void OnWindowMouseUp(MouseButtonEventArgs e) { }
    protected virtual void OnWindowMouseMove(MouseMoveEventArgs e) { }
    protected virtual void OnWindowMouseWheel(MouseWheelEventArgs e) { }
}
