#version 330 core

layout (location = 0) out vec4 frag_colour;
uniform float time;

void main() {
  frag_colour = vec4(0.5, sin(time) / 2.0 + 0.5 , 0.5, 1.0);
}
