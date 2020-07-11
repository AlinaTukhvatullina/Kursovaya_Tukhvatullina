#version 330 core
layout (location = 0) out vec3 gPosition;
layout (location = 1) out vec3 gNormal;
layout (location = 2) out vec4 gAlbedoSpec;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;

struct Material {
    sampler2D diffuseMap;
    sampler2D specularMap;
    float shininess;
};

uniform Material material;

void main()
{   
    gPosition = FragPos;
    gNormal = normalize(Normal);
    gAlbedoSpec.rgb = texture(material.diffuseMap, TexCoords).rgb;
    gAlbedoSpec.a = texture(material.specularMap, TexCoords).r;
}