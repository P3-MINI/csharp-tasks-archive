#version 330 core

uniform vec4 viewport;
uniform mat4 invViewProj;
uniform sampler2D sampler;

out vec4 FragColor;

uniform float time;
uniform vec3 color;
uniform vec3 ambient;

void main() {
    vec4 ndc;
    ndc.xy = ((2.0 * gl_FragCoord.xy) - (2.0 * viewport.xy)) / (viewport.zw) - 1;
    ndc.z = (2.0 * gl_FragCoord.z - gl_DepthRange.near - gl_DepthRange.far) / (gl_DepthRange.far - gl_DepthRange.near);
    ndc.w = 1.0;

    vec4 clip = invViewProj * ndc;
    vec3 world = (clip / clip.w).xyz;
    vec2 uv = fract(world.xz);

    int frame = int(time);
    float mix = time - frame;
    vec3 tex1 = texture(sampler, vec2(uv.x, (uv.y + frame) / 32)).rgb;
    vec3 tex2 = texture(sampler, vec2(uv.x, (uv.y + frame + 1) / 32)).rgb;
    FragColor = vec4((tex1 * (1 - mix) + tex2 * mix) * color * ambient, 1.0);
}