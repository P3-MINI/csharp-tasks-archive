#include <iostream>
#include "Resource.hpp"

Resource::Resource(std::string name) : _name(name)
{
	ResourceGuard::GetInstance()._registered.insert(std::make_pair(this, name));
}

Resource::~Resource()
{
	auto it = ResourceGuard::GetInstance()._registered.find(this);
	if (it == ResourceGuard::GetInstance()._registered.end())
	{
		std::cerr << "ERROR: deleting unregistered resource.\n";
		return;
	}
	ResourceGuard::GetInstance()._registered.erase(it);
}

void * Resource::operator new(size_t size)
{
	Resource *ptr = reinterpret_cast<Resource*>(malloc(size));
	if (!ptr)
	{
		std::cerr << "ERROR: Cannot allocate memory for resource.\n";
		return nullptr;
	}
	ResourceGuard::GetInstance()._allocated.insert(ptr);
	return ptr;
}

void Resource::operator delete(void * ptr)
{
	Resource *ptr_res = reinterpret_cast<Resource*>(ptr);
	if (!ptr_res)
	{
		std::cerr << "ERROR: Cannot delete nullptr!\n";
		return;
	}
	auto it = ResourceGuard::GetInstance()._allocated.find(ptr);
	if (it == ResourceGuard::GetInstance()._allocated.end())
	{
		std::cerr << "ERROR: Resource was already deleted!\n";
		return;
	}
	ResourceGuard::GetInstance()._allocated.erase(it);
	free(ptr);
}

const std::string & Resource::GetName() const
{
	return _name;
}

ResourceGuard::~ResourceGuard()
{
	if (_allocated.size() > 0)
	{
		std::cerr << "\nERROR: following resources weren't deleted:\n";
		for (auto& elem : _registered)
			std::cerr << "    + " << elem.second << "\n";
	}
	else
		std::cout << "\nAll resources were released.\n";
}

ResourceGuard & ResourceGuard::GetInstance()
{
	static ResourceGuard guard;
	return guard;
}

