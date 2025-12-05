using OpenTK.Graphics.OpenGL4;

namespace OpenGL;

public class Framebuffer : IBindable, IDisposable
{
    public int Handle { get; }
    private Dictionary<FramebufferAttachment, Texture> Attachments { get; } = new();

    public Framebuffer()
    {
        GL.CreateFramebuffers(1, out int handle);
        Handle = handle;
    }

    public void AttachTexture(FramebufferAttachment attachment, Texture texture, int level = 0)
    {
        Attachments[attachment] = texture;
        GL.NamedFramebufferTexture(Handle, attachment, texture.Handle, level);
    }

    public void CheckStatus()
    {
        var status = GL.CheckNamedFramebufferStatus(Handle, FramebufferTarget.Framebuffer);
        if (status != FramebufferStatus.FramebufferComplete)
        {
            throw new Exception($"Framebuffer is incomplete: {status}");
        }
    }

    public Texture GetTexture(FramebufferAttachment attachment)
    {
        return Attachments[attachment];
    }

    public void Bind()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
    }

    public void Unbind()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public void Dispose()
    {
        foreach (var attachment in Attachments)
        {
            attachment.Value.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}