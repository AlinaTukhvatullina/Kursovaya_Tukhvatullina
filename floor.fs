#version 330 core
out vec4 FragColor;

struct PointLight {
    vec3 position;
    float constant;
    float linear;
    float quadratic;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

struct DirLight {
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

#define NR_POINT_LIGHTS 32

in vec2 TexCoords;

uniform vec3 viewPos;
uniform DirLight dirLight;

uniform PointLight pointLights[NR_POINT_LIGHTS];
uniform sampler2D gPosition;
uniform sampler2D gNormal;
uniform sampler2D gAlbedoSpec;

vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir, vec3 Diffuse, float Specular);
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir, vec3 Diffuse, float Specular);

void main()
{    
    
   vec3 FragPos = texture(gPosition, TexCoords).rgb;
   vec3 norm = texture(gNormal, TexCoords).rgb;
   vec3 Diffuse = texture(gAlbedoSpec, TexCoords).rgb;
   float Specular = texture(gAlbedoSpec, TexCoords).a;


   vec3 viewDir = normalize(viewPos - FragPos);
   
   vec3 result = CalcDirLight(dirLight, norm, viewDir, Diffuse, Specular);
   for (int i=0; i<NR_POINT_LIGHTS; i++)
   result += CalcPointLight(pointLights[i], norm, FragPos, viewDir, Diffuse, Specular);    
   
   FragColor = vec4(result, 1.0);
}



vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir, vec3 Diffuse, float Specular)
{
    vec3 lightDir = normalize(-light.direction);

    float diff = max(dot(normal, lightDir), 0.0);

    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);

    vec3 ambient = light.ambient * Diffuse;
    vec3 diffuse = light.diffuse * diff * Diffuse;
    vec3 specular = light.specular * spec * Specular;
    return (ambient + diffuse + specular);
}


vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir, vec3 Diffuse, float Specular)
{
    vec3 lightDir = normalize(light.position - fragPos);

    float diff = max(dot(normal, lightDir), 0.0);

    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);

    float distance = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

    vec3 ambient = light.ambient * Diffuse;
    vec3 diffuse = light.diffuse * diff * Diffuse;
    vec3 specular = light.specular * spec * Specular;
    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;
    return (ambient + diffuse + specular);
}


