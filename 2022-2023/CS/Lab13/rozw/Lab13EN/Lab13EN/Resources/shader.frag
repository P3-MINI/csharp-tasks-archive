#version 330 core

uniform sampler2D tex;

out vec4 FragColor;

in VertexData {
    vec3 normal;
    vec3 position;
    vec2 tc;
} fs_in;

vec3 lightPos = vec3(0, 20, 0);

void main() {
    vec3 lightDir = normalize(lightPos - fs_in.position);
    vec3 normal = normalize(fs_in.normal);
    float diffuse = max(dot(normal, lightDir), 0.0);
    vec3 color = texture(tex, fs_in.tc).rgb;
    FragColor = vec4(vec3(diffuse + 0.2) * color, 1.0f);
}