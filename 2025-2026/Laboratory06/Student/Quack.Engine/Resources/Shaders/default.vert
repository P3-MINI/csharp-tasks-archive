#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 tc;

uniform mat4 mvp;
uniform mat4 model;

out VertexData {
    vec3 normal;
    vec2 tc;
} vs_out;

void main() {
    vs_out.normal = vec3(model * vec4(normal, 0.0));
    vs_out.tc = tc;
    gl_Position = mvp * vec4(position, 1.0);
}
