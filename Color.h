#pragma once

class Color
{
public:
	float red;
	float green;
	float blue;
	float special;
	Color() {

	};
	Color(float r, float g, float b, float s) {
		red = r;
		green = g;
		blue = b;
		special = s;
	}
};

