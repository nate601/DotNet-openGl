#version 330 core

layout (location = 0) in vec3 vp;
layout (location = 1) in vec2 aTexCoord;

uniform mat4 matrixTest;

out vec2 TexCoord;

void main() {
  gl_Position = vec4(vp, 1.0) * matrixTest;
  TexCoord = aTexCoord;
}

