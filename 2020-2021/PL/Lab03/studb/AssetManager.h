#ifndef ADDET_MANAGER_HEADER
#define ADDET_MANAGER_HEADER

#include <string>
#include <vector>
#include <type_traits>

struct Asset
{
    std::string GetName() { return this->m_AssetName; }

    Asset(const std::string& assetName) : m_AssetName(assetName) { }

protected: std::string m_AssetName;
};

struct SoundAsset : public Asset
{
    SoundAsset(const std::string& assetName) : Asset(assetName) { }
};

struct MeshAsset : public Asset
{
    MeshAsset(const std::string& assetName) : Asset(assetName) { }
};

class AssetManager
{
    std::vector<std::unique_ptr<Asset>> m_Assets;

public:

    /* Implement Template Change Here */

    template <typename Type>
    void AddAsset(std::unique_ptr<Type> newAsset)
    {
        this->m_Assets.push_back(std::move(newAsset));
    }
};

#endif /* ADDET_MANAGER_HEADER */
