#version 330 core

in vec2 TexCoord;

out vec4 FragColor;

uniform sampler2D sampler;
uniform float time;
uniform vec3 color;

void main() {
    int frame = int(time);
    float mix = time - frame;
    vec3 tex1 = texture(sampler, vec2(TexCoord.x, (TexCoord.y + frame) / 32)).rgb;
    vec3 tex2 = texture(sampler, vec2(TexCoord.x, (TexCoord.y + frame + 1) / 32)).rgb;
    FragColor = vec4((tex1 * (1 - mix) + tex2 * mix) * color, 1.0);
}