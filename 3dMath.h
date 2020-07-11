#pragma once
#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <iostream>
#include <SOIL.h>
#include <stb_image_aug.h>

#include <glm.hpp>
#include <vec2.hpp>
#include <vec3.hpp>
#include <vec4.hpp>
#include <mat4x4.hpp>
#include <gtc/matrix_transform.hpp>
#include <gtc/type_ptr.hpp>

using namespace glm;

struct Vector2f {
	GLfloat x;
	GLfloat y;

	Vector2f() {

	}

	Vector2f(float _x, float _y) {
		x = _x;
		y = _y;
	}
};



struct vertexNoTex {
	vec3 position;
	vec3 color;

	vertexNoTex() {

	}

	vertexNoTex(vec3 pos, vec3 col) {
		position = pos;
		color = col;
	}
};

struct vertex {
	vec3 position;
	vec3 color;
	vec2 Texcoord;

	vertex() {

	}

	vertex(vec3 pos, vec3 col, vec2 tex) {
		position = pos;
		color = col;
		Texcoord = tex;
	}
};

struct Texture {
	int width;
	int height;

	unsigned char* image;

	Texture() {
		width = 0;
		height = 0;
	};

	Texture(const char* n) {
		image = SOIL_load_image(n, &width, &height, 0, SOIL_LOAD_RGB);
	}
};


struct material {
	Texture diffuse;
	Texture specular;
	Texture normal;
	float shininess;

	material() {
		normal = Texture();
		shininess = 32.0f;
	}
};

struct DirLight {
	vec3 direction;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct PointLight {
	vec3 position;
	float constant;
	float linear;
	float quadratic;
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct SpotLight {
	vec3 position;
	vec3 direction;
	float cutOff;
	float outerCutOff;

	float constant;
	float linear;
	float quadratic;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct Material {
	vec3 diffuse;
	vec3 specular;
	vec3 ambient;
	float shininess;
};