using OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Duck;

public class Sky : IDisposable
{
    private Mesh Quad { get; }
    private Shader Shader { get; }

    private Vector3 _a, _b, _c, _d, _e, _z;
    private const float NormalizedSunY = 1.15f;
    private float _turbidity = 2.6f;
    public Vector3 SunDir { get; set; } = Vector3.UnitY;
    public Vector3 Ambient { get; set; } = Vector3.One;

    public Sky()
    {
        var ibo = new IndexBuffer(new [] { 0, 1, 2, 3 }, 16, DrawElementsType.UnsignedInt, 4);
        Quad = new Mesh(PrimitiveType.TriangleStrip, ibo);
        Shader = new Shader(("OpenGL.Resources.Shaders.sky.frag", ShaderType.FragmentShader),
            ("OpenGL.Resources.Shaders.sky.vert", ShaderType.VertexShader));
    }

    public void Update(DateTime dateTime)
    {
        SunDir = TimeToSunDirection(dateTime);
        Ambient = AmbientColor(dateTime);
        float sunTheta = float.Acos(float.Clamp(SunDir.Y, 0.0f, 1.0f));

        // A.2 Skylight Distribution Coefficients and Zenith Values: compute Perez distribution coefficients
        _a = new Vector3(-0.0193f, -0.0167f,  0.1787f) * _turbidity + new Vector3(-0.2592f, -0.2608f, -1.4630f);
        _b = new Vector3(-0.0665f, -0.0950f, -0.3554f) * _turbidity + new Vector3( 0.0008f,  0.0092f,  0.4275f);
        _c = new Vector3(-0.0004f, -0.0079f, -0.0227f) * _turbidity + new Vector3( 0.2125f,  0.2102f,  5.3251f);
        _d = new Vector3(-0.0641f, -0.0441f,  0.1206f) * _turbidity + new Vector3(-0.8989f, -1.6537f, -2.5771f);
        _e = new Vector3(-0.0033f, -0.0109f, -0.0670f) * _turbidity + new Vector3( 0.0452f,  0.0529f,  0.3703f);

        // A.2 Skylight Distribution Coefficients and Zenith Values: compute zenith color
        _z.X = ZenithChromacity(new Vector4(0.00166f, -0.00375f, 0.00209f, 0), new Vector4(-0.02903f, 0.06377f, -0.03202f, 0.00394f), new Vector4(0.11693f, -0.21196f, 0.06052f, 0.25886f), sunTheta, _turbidity);
        _z.Y = ZenithChromacity(new Vector4(0.00275f, -0.00610f, 0.00317f, 0), new Vector4(-0.04214f, 0.08970f, -0.04153f, 0.00516f), new Vector4(0.15346f, -0.26756f, 0.06670f, 0.26688f), sunTheta, _turbidity);
        _z.Z = ZenithLuminance(sunTheta, _turbidity);
        _z.Z *= 1000; // conversion from kcd/m^2 to cd/m^2

        // 3.2 Skylight Model: pre-divide zenith color by distribution denominator
        _z.X /= Perez(0, sunTheta, _a.X, _b.X, _c.X, _d.X, _e.X);
        _z.Y /= Perez(0, sunTheta, _a.Y, _b.Y, _c.Y, _d.Y, _e.Y);
        _z.Z /= Perez(0, sunTheta, _a.Z, _b.Z, _c.Z, _d.Z, _e.Z);

        // For low dynamic range simulation, normalize luminance to have a fixed value for sun
        if (NormalizedSunY != 0) _z.Z = NormalizedSunY / Perez(sunTheta, 0, _a.Z, _b.Z, _c.Z, _d.Z, _e.Z);
    }

    public void Render(Camera camera)
    {
        Shader.Use();
        
        Shader.LoadFloat3("SunDir", SunDir.Normalized());
        Shader.LoadMatrix4("InvViewProj", camera.ProjectionViewMatrix.Inverted());
        Shader.LoadFloat3("p_A", _a);
        Shader.LoadFloat3("p_B", _b);
        Shader.LoadFloat3("p_C", _c);
        Shader.LoadFloat3("p_D", _d);
        Shader.LoadFloat3("p_E", _e);
        Shader.LoadFloat3("p_Z", _z);
        
        Quad.Bind();
        Quad.RenderIndexed();
    }
    
    private float ZenithChromacity(Vector4 c0, Vector4 c1, Vector4 c2, float sunTheta, float turbidity)
    {
        Vector4 thetav = new Vector4(sunTheta * sunTheta * sunTheta, sunTheta * sunTheta, sunTheta, 1);
        return Vector3.Dot(new Vector3(turbidity * turbidity, turbidity, 1), new Vector3(Vector4.Dot(thetav, c0), Vector4.Dot(thetav, c1), Vector4.Dot(thetav, c2)));
    }

    private float ZenithLuminance(float sunTheta, float turbidity)
    {
        float chi = (4.0f / 9.0f - turbidity / 120) * (float.Pi - 2 * sunTheta);
        return (4.0453f * turbidity - 4.9710f) * float.Tan(chi) - 0.2155f * turbidity + 2.4192f;
    }

    private float Perez(float theta, float gamma, float a, float b, float c, float d, float e)
    {
        return (1.0f + a * float.Exp(b / (float.Cos(theta) + 0.01f))) * (1.0f + c * float.Exp(d * gamma) + e * float.Cos(gamma) * float.Cos(gamma));
    }

    private Vector3 TimeToSunDirection(DateTime dateTime)
    {
        // Convert the time of day to a float representing the proportion of the day.
        TimeSpan timeOfDay = dateTime.TimeOfDay;
        float hours = (float)timeOfDay.TotalHours;

        // Calculate the angle of the sun in radians.
        // 0.0 corresponds to midnight, 0.25 to 6am, 0.5 to noon, 0.75 to 6pm, 1.0 to midnight
        float t = hours / 24f;

        // Angle in radians, where 0 is midnight, and progresses through the day.
        float angle = t * 2f * MathF.PI + MathF.PI / 2;

        // Calculate our tilt: let's assume a slight 23-degree tilt from the orthogonal plane.
        float tiltAngle = MathHelper.DegreesToRadians(23f);

        // Calculate the base sun position on the x/y plane without tilt.
        Vector3 baseSunPosition = new Vector3(MathF.Cos(angle), MathF.Sin(angle), 0f);

        // Apply the tilt by rotating around the x-axis.
        Matrix3 tiltMatrix = Matrix3.CreateRotationX(tiltAngle);
        Vector3 tiltedSunPosition = baseSunPosition * tiltMatrix;

        // Normalize the resulting vector to get the direction.
        tiltedSunPosition.Normalize();

        return -tiltedSunPosition;
    }

    public static Vector3 AmbientColor(DateTime dateTime)
    {
        if (dateTime is { Hour: 21, Minute: 37 }) return new Vector3(1, 0.7f, 0);
        TimeSpan timeOfDay = dateTime.TimeOfDay;
        float hours = (float)timeOfDay.TotalHours;// Define colors for specific times
        Vector3[] colors = new Vector3[]
        {
            new Vector3(0.1f, 0.1f, 0.3f), // 0:00
            new Vector3(0.2f, 0.2f, 0.4f), // 3:00
            new Vector3(0.8f, 0.5f, 0.5f), // 6:00
            new Vector3(0.8f, 0.8f, 1.0f), // 9:00
            new Vector3(1.0f, 1.0f, 1.0f), // 12:00
            new Vector3(1.0f, 1.0f, 1.0f), // 15:00
            new Vector3(0.8f, 0.5f, 0.5f), // 18:00
            new Vector3(0.2f, 0.2f, 0.4f)  // 21:00
        };

        // Calculate the indices for the surrounding key times
        int prevIndex = (int)(hours / 3) % colors.Length;
        int nextIndex = (prevIndex + 1) % colors.Length;

        // Calculate the interpolation factor
        float t = (hours % 3) / 3f;

        // Lerp between the two colors
        Vector3 startColor = colors[prevIndex];
        Vector3 endColor = colors[nextIndex];
        
        Vector3 ambientColor = startColor * (1 - t) + endColor * t;
        return ambientColor;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Shader.Dispose();
        Quad.Dispose();
    }
}