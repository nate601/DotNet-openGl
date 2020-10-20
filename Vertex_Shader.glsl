#version 330 core

layout (location = 0) in vec3 vp;
layout (location = 1) in vec2 aTexCoord;

uniform vec3 vectorTest;

out vec2 TexCoord;

void main() {
  gl_Position = vec4(vp * vectorTest, 1.0);
  TexCoord = aTexCoord;
}

