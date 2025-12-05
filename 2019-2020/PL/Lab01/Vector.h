#pragma once
#include <ostream>
#include <string>
#include <exception>

template<class T>
T min(T v1, T v2) { return v1 < v2 ? v1 : v2; }

template<class T>
T max(T v1, T v2) { return v1 > v2 ? v1 : v2; }

template<class T = float>
struct Vector
{
private:
	T* values;
	int size;

public:

	Vector(int _size = 3) :size(_size)
	{
		values = new T[size];

		for (int index = 0; index < size; index++)
			values[index] = T();     // <-- tu mog¹ mieæ problem - podpowiedzieæ
	}

	template<unsigned int N>
	Vector(const T(&_tab)[N]) :size(N)
	{
		values = new T[size];

		for (int index = 0; index < size; index++)
			values[index] = _tab[index];
	}

	Vector(const Vector& vec)
	{
		size = vec.size;
		values = new T[size];

		for (int index = 0; index < size; index++)
			values[index] = vec.values[index];
	}

	~Vector()
	{
		delete[] values;
	}

	Vector& operator=(const Vector& vec)
	{
		if (this == &vec) return *this;

		if (values != nullptr)
			delete[] values;

		size = vec.size;
		values = new T[size];

		for (int index = 0; index < size; index++)
			values[index] = vec.values[index];

		return *this;
	}

	T& operator[](const int index) const
	{
		return values[index];
	}

	friend T Dot(const Vector<T>& vec1, const Vector<T>& vec2)
	{
		if (vec1.size != vec2.size)
			throw std::exception("Niezgodnosc rozmiarow");

		T result = 0;

		for (int index = 0; index < vec1.size; index++)
			result += vec1[index] * vec2[index];

		return result;
	}

	friend Vector<T> Clamp(const Vector<T>& vec, T min_value, T max_value)
	{
		Vector<T> result(vec.size);

		for (int index = 0; index < result.size; index++)
			result[index] = max(min(vec[index], max_value), min_value);

		return result;
	}

	friend float Length(const Vector<T>& vec)
	{
		float length = 0;

		for (int index = 0; index < vec.size; index++)
			length += vec[index]*vec[index];

		return std::sqrt(length);
	}

	friend Vector<float> Normalize(const Vector<T>& vec)
	{
		Vector<float> result(vec.size);
		float length = Length(vec);

		for (int index = 0; index < result.size; index++)
			result[index] = vec[index] / length;

		return result;
	}

	friend Vector<T> Cross(const Vector<T>& vec1, const Vector<T>& vec2)
	{
		if (vec1.size != 3 || vec2.size != 3)
			throw std::exception("Zly rozmiar wektorow");

		Vector<T> result(3);

		result[0] = vec1[1] * vec2[2] - vec1[2] * vec2[1];
		result[1] = vec1[2] * vec2[0] - vec1[0] * vec2[2];
		result[2] = vec1[0] * vec2[1] - vec1[1] * vec2[0];

		return result;
	}

	friend std::ostream& operator<<(std::ostream& out, const Vector<T>& vector)
	{
		out << '[';

		if (vector.size > 0)
		{
			out << vector.values[0];

			for (int index = 1; index < vector.size; index++)
				out << ';' << vector.values[index];
		}

		out << ']';

		return out;
	}
};