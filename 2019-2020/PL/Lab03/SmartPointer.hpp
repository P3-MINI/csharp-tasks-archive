#ifndef SMARTPOINTER_HPP
#define SMARTPOINTER_HPP

template<typename T>
class SmartPointer
{
	T* _ptr;
public:
	SmartPointer(T *ptr)
	{
		_ptr = ptr;
	}
	SmartPointer(SmartPointer&& sptr)
	{
		if (this == &sptr)
			return;
		_ptr = sptr._ptr;
		sptr._ptr = nullptr;
	}
	SmartPointer& operator =(SmartPointer&& sptr)
	{
		if (this == &sptr)
			return *this;
		if (_ptr)
			delete _ptr;
		_ptr = sptr._ptr;
		sptr._ptr = nullptr;
		return *this;
	}
	~SmartPointer()
	{
		if (_ptr)
			delete _ptr;
	}
	T& operator*()
	{
		return *_ptr;
	}
	T* operator->() const
	{
		return _ptr;
	}
};
#endif
