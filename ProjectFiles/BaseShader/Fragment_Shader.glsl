#version 330 core

layout (location = 0) out vec4 frag_color;
uniform float time;
uniform sampler2D tex;

in vec2 TexCoord;

void main() {
  frag_color = texture(tex, TexCoord) * vec4(0.5,sin(time) / 2.0 + 0.5, 0.5 , 1.0);
  /* frag_colour = vec4(0.5, sin(time) / 2.0 + 0.5 , 0.5, 1.0); */
}
