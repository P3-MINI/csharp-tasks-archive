#pragma once
#include <cstdint>
#include <string>
#include <set>
#include <map>

class Resource;

class ResourceGuard
{
	std::set<void*> _allocated;
	std::map<void*, std::string> _registered;

	ResourceGuard() = default;
	~ResourceGuard();

	ResourceGuard(const ResourceGuard &) = delete;
	void operator=(const ResourceGuard &) = delete;

	friend class Resource;
public:
	static ResourceGuard& GetInstance();
};

class Resource
{
	std::string _name;
public:
	Resource(std::string name);
	~Resource();

	void* operator new(size_t size);
	void operator delete(void* ptr);

	const std::string& GetName() const;
};
