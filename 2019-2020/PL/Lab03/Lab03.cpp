#include <iostream>
#include <algorithm>
#include <vector>
#include <string>
#include <cctype>
#include "Resource.hpp"
#include "SmartPointer.hpp"

#define STAGE1
#define STAGE2
#define STAGE3

#ifdef STAGE2
std::tuple<std::vector<SmartPointer<Resource>>, std::vector<SmartPointer<Resource>>> SplitByArchitecture(std::vector<SmartPointer<Resource>>& resources)
{
	const char* pattern = "x64";
	const char* endPattern = pattern + std::strlen(pattern);
	std::vector<SmartPointer<Resource>> x86, x64;
	//copy_if nie zadziala, poniewaz SmartPointer nie posiada konstruktora kopiujacego
	for (auto& p : resources)
	{
		auto found = std::search(p->GetName().begin(), p->GetName().end(), pattern, endPattern);
		if (found != p->GetName().end())
			x64.push_back(std::move(p));
		else
			x86.push_back(std::move(p));
		//potrzebne jest explicit std::move w push_back
	}
	//tu rowniez bez std::move kompilacja sie nie powiedzie
	return std::make_tuple(std::move(x86), std::move(x64));
}
#endif

SmartPointer<Resource> operator "" _RESOURCE(const char* c_str, size_t size)
{
	auto front = c_str;
	auto back = c_str + size - 1;
	bool palindrom = true;
	while (palindrom && front < back)
		if (std::tolower(*front++) != std::tolower(*back--))
			palindrom = false;
	if (palindrom)
		return new Resource(std::string(c_str) + "_DEPRECATED");
	else
		return new Resource(c_str);
}

#if defined(STAGE2) || defined(STAGE3)
void ShowResources(std::vector<SmartPointer<Resource>>& resources)
{
	for (auto& p : resources)
		if (&*p != nullptr)
			std::cout << p->GetName() << std::endl;
		else
			std::cout << "**Rleased resource**\n";
}
#endif

void PrintStage(const std::string& str);

int main(int argc, char** argv)
{
	// Przy zaliczeniu kazdego etapu wszystkie zasoby powinny byc zwolnione
	// przed zakonczeniem wykonywania programu.
	// Jest to wymagane do otrzymania punktow za etapy.

#ifdef STAGE1
	PrintStage("STAGE 1 (2.0 pt)");
	// Zaimplementuj (w samodzielnie utworzonym pliku SmartPointer.hpp)
    // szablonowa klase SmartPointer<T> przechowujaca wskaznik do obiektu typu T
	// Klasa ta powinna zachowywac sie podobnie jak STL klasa std::unique_ptr
	// SmartPointer nie moze byc kopiowany, a jedynie przenoszony
	// Trzeba zapewnic by w danej chwili istnial tylko jeden SmartPointer
	// z danym wskaznikiem
	// Gdy SmartPointer jest niszczony, nalezy zwolnic zasoby ktore przechowuje
	// Klasa SmartPointer powinna rowniez zachowywac sie jak wskaznik do typu T
	// przy korzystaniu z operatorow * oraz ->
    // Zaimplementuj wszystkie niezbędne elementy klasy i zadbaj aby kompilator
    // nie tworzył automatycznie elementów nieporządanych
	const char *FH_str = "File_Handler";
	const char *SH_str = "Window_Handler";
	std::cout << "Creating resource of " << FH_str << "\n";
	Resource *FH_res = new Resource(FH_str);
	std::cout << "Creating resource of " << SH_str << "\n";
	Resource *SH_res = new Resource(SH_str);
	std::cout << "Constructor\n";
	SmartPointer<Resource> sp1(FH_res);
	std::cout << "Move operator\n";
	sp1 = std::move(sp1);
	std::cout << "Constructor\n";
	SmartPointer<Resource> sp2(SH_res);
	std::cout << "Variable sp2 holds resource \"" << (*sp2).GetName() << "\"\n";
	std::cout << "Move constructor\n";
	SmartPointer<Resource> sp3(std::move(sp1));
	std::cout << "Move operator\n";
	sp2 = std::move(sp3);
	std::cout << "Variable sp2 holds resource \"" << sp2->GetName() << "\"\n";
#endif

#ifdef STAGE2
	PrintStage("STAGE 2 (1.5 pt)");
	// Zaimplementuj (na początku pliku Lab03.cpp) funkcje SplitByArchitecture
	// Jej zadaniem jest podzial wektora std::vectora<SmartPointer<Resource>>
	// na dwa osobne wektory x86 oraz x64
	// W wektorze x64 nalezy umiescic wszystkie te wskazniki, ktore
	// wksazuja na Resource zawierajacy w nazwie 'x64'
	// pozostale nalezy umiescic w drugim wektorze x86
	auto resouces_str = {
		"CD_Driver_x86",
		"USB_Driver",
		"WinApi_Dll",
		"CUDADrivers_10.1",
		"GPU_Driver_x64",
		"WinApi_Dllx64",
		"Gamepad_Driver",
		"OpenCLx64_WinPlatform",
	};
	std::vector<SmartPointer<Resource>> resources, x86, x64;
	std::transform(resouces_str.begin(), resouces_str.end(), std::back_inserter(resources), [](const char* c_str) { return new Resource(c_str); });
	std::cout << "\nAll resources:\n";
	ShowResources(resources);

	std::tie(x86, x64) = SplitByArchitecture(resources);

	std::cout << "\nOld resources:\n";
	ShowResources(resources);

	std::cout << "\nx86 resources:\n";
	ShowResources(x86);

	std::cout << "\nx64 resources:\n";
	ShowResources(x64);
#endif

#ifdef STAGE3
	PrintStage("STAGE 3 (1.5 pt)");
	// W C++ istnieja tak zwane literaly
	// sluza one by w wygodny sposob definiowac zmienne roznych typow
	// Przykladami uzycia literalu sa:
	//   100ull - zwracajacy 100 jako unsigned long long
	//   5.902e2f - zwracajacy 590.2 jako float
	// Zaimplementuj (na początku pliku Lab03.cpp) wlasny literal _RESOURCE
	// Powinien zamieniac on dany lancuch znakow na SmartPointer<Resource> z podana nazwa
	// Dodatkowo jesli string jest palindromem nalezy dodac do konca nazwy "_DEPRECATED".
	// Przy sprawdzaniu palindromu ignoruj wielkosc liter, oznacza to ze "TopSpot" jest palindromem.
	// Naglowek funkcji definujacej literal to:
	// ZWRACANY_TYP operator "" LITERAL(PARAMETRY)
	// Gdzie:
	//   ZWRACANY_TYP to typ jaki powinien zwrocic nasz literal
	//   LITERAL to przyrostek jakim bedziemy wywolywac literal (w naszym przypadku _RESOURCE)
	//   PARAMETRY bede definiowac do jakich wartosci chcemy przypisac literal np.
	//     dla liczb calkowitych bedzie to (unsigned long long int)
	//     dla lancucha znakow bedzie to (const char*, std::size_t)
	// Wiecej informacji znajduje sie na cppreference
	// http://cppreference.mini.pw.edu.pl/w/cpp/language/user_literal
	// https://en.cppreference.com/w/cpp/language/user_literal

	std::vector<SmartPointer<Resource>> customResources;
	customResources.push_back("honda"_RESOURCE);
	customResources.push_back("civic"_RESOURCE);
	customResources.push_back("NoLemon_NoMelon"_RESOURCE);
	customResources.push_back("Ott0"_RESOURCE);
	customResources.push_back("TopSpot"_RESOURCE);
	customResources.push_back("NotAPalindrome"_RESOURCE);
	customResources.push_back("7RADar7"_RESOURCE);

	std::cout << "\nCustom resources:\n";
	ShowResources(customResources);
#endif
	PrintStage("END");
	return 0;
}

void PrintStage(const std::string& str)
{
	std::cout << "\n" << std::string(8, '>') << " " << str << " " << std::string(8, '<') << "\n\n";
}
