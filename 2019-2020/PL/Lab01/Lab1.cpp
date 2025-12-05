#include <iostream>
#include <exception>
#include "Vector.h"

//Należy odkomentować w etapie 3
template<class T, int N>
Vector<T>* Sort(Vector<T>(&vectors)[N])
{
	Vector<T>* result = new Vector<T>[N];
	int* indices = new int[N];
	float* length = new float[N];

	for (int index = 0; index < N; index++)
	{
		indices[index] = index;
		length[index] = Length(vectors[index]);
	}


	for (int index_i = 0; index_i < N; index_i++)
		for (int index_j = index_i + 1; index_j < N; index_j++)
		{
			if (length[indices[index_j]] < length[indices[index_i]])
			{
				int temp = indices[index_i];
				indices[index_i] = indices[index_j];
				indices[index_j] = temp;
			}
		}

	for (int index = 0; index < N; index++)
	{
		result[index] = vectors[indices[index]];
	}

	delete[] indices;
	delete[] length;

	return result;
}

int main()
{
	std::cout << "**********ETAP 1**************\n\n";

	float tab1[3] = { 1.5f, 2.3f, 3.14f };
	int tab2[2] = { 10, 15 };

	Vector<float> vec1(tab1);
	Vector<int> vec2(tab2);
	Vector<double> vec3(5);

	std::cout << vec1 << " Powinno byc: [1.5;2.3;3.14]" << std::endl;
	std::cout << vec2 << " Powinno byc: [10;15]" << std::endl;
	std::cout << vec3 << " Powinno byc: [0;0;0;0;0]" << std::endl;

	std::cout << "\n**********ETAP 2**************\n\n";

	float tab4[3] = { 1.7f, 2.4f, 3.15f };
	float tab5[6] = { 1.7f, 2.4f, 3.15f, 1.0f, 2.4f, 3.56f };
	int tab6[3] = { 1,2,3 };
	int tab7[3] = { 4,5,6 };

	Vector<float> vec4(tab4);
	Vector<float> vec5(tab5);
	Vector<int> vec6(tab6);
	Vector<int> vec7(tab7);

	try
	{
		std::cout << Dot(vec6, vec6) << " Powinno byc: 14" << std::endl;
		std::cout << Clamp(vec5, 1.8f, 3.0f) << " Powinno byc: [1.8;2.4;3;1.8;2.4;3]" << std::endl;
		std::cout << Length(vec1) << " Powinno byc: 4.17128" << std::endl;
		std::cout << Normalize(vec4) << " Powinno byc: [0.39447;0.556898;0.730929]" << std::endl;
		std::cout << Cross(vec6, vec7) << " Powinno byc: [-3;6;-3]" << std::endl;
		std::cout << Dot(vec2, vec6) << " Powinien zlapac wyjatek" << std::endl;
	}
	catch (std::exception ex)
	{
		std::cout << ex.what() << std::endl;
	}

	std::cout << "\n**********ETAP 3**************\n";

	int tab8[3] = { 2,-2,6 };
	int tab9[3] = { 12,-22,36 };
	int tab10[3] = { 1,-1,2 };

	Vector<int> vectors[7];
	vectors[0] = vec6;
	vectors[1] = vec7;
	vectors[2] = Vector<int>(tab8);
	vectors[3] = vec6;
	vectors[4] = Vector<int>(3);
	vectors[5] = Vector<int>(tab9);
	vectors[6] = Vector<int>(tab10);

	Vector<int>* sorted_vectors = Sort(vectors);

	for (int index = 0; index < 7; index++)
	{
		std::cout << sorted_vectors[index] << std::endl;
	}

	std::cout << "Powinno byc:" << std::endl;

	std::cout << vectors[4] << std::endl;
	std::cout << vectors[6] << std::endl;
	std::cout << vectors[0] << std::endl;
	std::cout << vectors[3] << std::endl;
	std::cout << vectors[2] << std::endl;
	std::cout << vectors[1] << std::endl;
	std::cout << vectors[5] << std::endl;

	delete[] sorted_vectors;
}