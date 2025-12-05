#version 330 core

uniform sampler2D tex;

out vec4 FragColor;

in VertexData {
    vec3 normal;
    vec2 tc;
} fs_in;

uniform vec3 lightDir;
uniform vec3 lightColor;

void main() {
    vec3 normal = normalize(fs_in.normal);
    float diffuse = min(max(dot(normal, lightDir), 0.0) + 0.5, 1.0);
    vec3 color = texture(tex, fs_in.tc).rgb;
    FragColor = vec4(lightColor * color * diffuse, 1.0);
}